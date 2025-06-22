using System;
using System.Runtime.CompilerServices;
using Animancer;
using Fusion;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerAnimation: NetworkBehaviour
    {
        [SerializeField]
        private TransitionAsset m_Dash;
        
        [SerializeField]
        private TransitionAsset m_Death;

        [SerializeField]
        private TransitionAsset m_Revive;

        [SerializeField]
        private TransitionAsset m_Movement;

        [SerializeField]
        private TransitionAsset m_MovementAim;

        [SerializeField]
        private TransitionAsset m_Atk;

        private PlayerCharacter m_PlayerCharacter;
        private AnimancerComponent m_AnimancerComponent;
        private PlayerState m_PlayerState;

        private PlayerMovement m_PlayerMovement;

        /// <summary>
        /// 攻击有三种，显示具体攻击是哪一种
        /// </summary>
        [ShowInInspector,Sirenix.OdinInspector.ReadOnly]
        private int m_AtkIndex;

        private Subject<Unit> m_OnExitAtk = new();
        
        private void Awake()
        {
            m_PlayerCharacter = GetComponent<PlayerCharacter>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerState = GetComponent<PlayerState>();
            m_AnimancerComponent = m_PlayerCharacter.Model.GetComponent<AnimancerComponent>();
            m_PlayerMovement.OnVelocityChange += SetVelocity;
        }

        
        private void SetVelocity(int obj)
        {
            m_AnimancerComponent.Parameters.SetValue("Speed",(float)m_PlayerMovement.Velocity / Constraints.Character.SpeedScale);
        }

        private void Start()
        {
            PlayMovement();
        }

        [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.All)]
        public void RPC_PlayAtkRpc()
        {
            m_AnimancerComponent.Parameters.SetValue("AtkIndex",(float)m_AtkIndex);
            m_AnimancerComponent.Play(m_Atk);
            m_AtkIndex++;
            m_AtkIndex %= 3;
            float animTime = m_AnimancerComponent.Layers[0].CurrentState.Duration;
            var onAtkEnd = Observable.Merge(
                Observable.Timer(TimeSpan.FromSeconds(animTime)).Select(x=>Unit.Default),
                m_OnExitAtk.AsObservable());
            onAtkEnd.Subscribe(x => { PlayMovement(); });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PlayMovement()
        {
            m_AnimancerComponent.Play(m_Movement);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_ExitAtkAnimRpc()
        {
            m_OnExitAtk.OnNext(Unit.Default);
        }
    }
}