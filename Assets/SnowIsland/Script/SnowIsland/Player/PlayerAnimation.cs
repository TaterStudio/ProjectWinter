using System;
using System.Runtime.CompilerServices;
using Animancer;
using Fusion;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

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
        private TransitionAsset m_Interact;
       
        [SerializeField]
        private TransitionAsset m_Place;

        [SerializeField]
        private TransitionAsset m_Atk;

        [SerializeField]
        private TransitionAsset m_Use;

        private PlayerCharacter m_PlayerCharacter;
        private AnimancerComponent m_AnimancerComponent;
        private PlayerState m_PlayerState;

        private PlayerMovement m_PlayerMovement;

        /// <summary>
        /// 攻击有三种，显示具体攻击是哪一种
        /// </summary>
        [ShowInInspector,Sirenix.OdinInspector.ReadOnly]
        private int m_AtkIndex;

        public IObservable<Unit> OnExitAtk => m_OnExitAtk;
        private Subject<Unit> m_OnExitAtk = new();

        public IObservable<Unit> OnExitInteract => m_OnExitInteract;
        private Subject<Unit> m_OnExitInteract = new();

        [SerializeField]
        private AvatarMask m_UpperBodyLayerMask;
        private AnimancerLayer m_UpperBodyLayer;
        
        private void Awake()
        {
            m_PlayerCharacter = GetComponent<PlayerCharacter>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerState = GetComponent<PlayerState>();
            m_AnimancerComponent = m_PlayerCharacter.Model.GetComponent<AnimancerComponent>();
            m_PlayerMovement.OnVelocityChange += SetVelocity;
            m_UpperBodyLayer = m_AnimancerComponent.Layers[1];
            m_UpperBodyLayer.Mask = m_UpperBodyLayerMask;
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
            SendEventAfterAnimEnd(m_OnExitAtk);
        }

        private void SendEventAfterAnimEnd(Subject<Unit> endEvent)
        {
            float animTime = m_AnimancerComponent.Layers[0].CurrentState.Duration;
            var onAtkEnd = Observable.Timer(TimeSpan.FromSeconds(animTime));
            onAtkEnd.Subscribe(_ => endEvent.OnNext(Unit.Default));
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_Interact(int animationId)
        {
            m_AnimancerComponent.Parameters.SetValue("InteractIndex",(float)animationId);
            m_AnimancerComponent.Play(m_Interact);
            SendEventAfterAnimEnd(m_OnExitInteract);
        }
        
        
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_Place(int animationId)
        {
            m_AnimancerComponent.Parameters.SetValue("PlaceIndex",(float)animationId);
            m_AnimancerComponent.Play(m_Place);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void PlayMovement()
        {
            m_AnimancerComponent.Play(m_Movement);
        }

        [Rpc(sources: RpcSources.InputAuthority,targets: RpcTargets.All)]
        public void RPC_PlayMove()
        {
            PlayMovement();
        }
        
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_FakeDead()
        {
            m_AnimancerComponent.Play(m_Death);
        }
        
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_Revive()
        {
            m_AnimancerComponent.Play(m_Revive);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_SetDefault()
        {
            PlayMovement();
        }

        private void PlayUseAnim(int index)
        {
            m_AnimancerComponent.Parameters.SetValue("UseIndex",(float)index);
            m_UpperBodyLayer.Play(m_Use);
        }

        public float CurrentAnimationDuration(int i)
        {
            return m_AnimancerComponent.Layers[i].CurrentState.Duration;
        }
    }
}