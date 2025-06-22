using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerFakeDeadLocomotionState: PlayerBaseLocomotionState
    {
        private PlayerAnimation m_PlayerAnimation;
        private bool m_Lifting;
        private CancellationTokenSource m_CancelSource;
        private bool m_FirstTick;
        private bool m_ShouldReturn = false;
        protected override void Awake()
        {
            base.Awake();
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
        }

        protected override async void OnEnterState()
        {
            base.OnEnterState();
            m_FirstTick = true;
            m_Lifting = false;
            m_ShouldReturn = false;
            m_PlayerAnimation.RPC_FakeDead();
            await WaitForAnimationEnd(default);
            m_FirstTick = false;
        }

        protected override void OnExitState()
        {
            m_CancelSource?.Cancel();
            m_CancelSource = null;
        }

        protected override void OnFixedUpdate()
        {
            if (m_FirstTick)
            {
                return;
            }

            if (m_ShouldReturn)
            {
                Back2Normal();
            }
            
            base.OnFixedUpdate();

            if (m_Lifting)
            {
                return;
            }
            
            bool fakeDeath = m_InputData.NetworkButtons.IsSet(PlayerInputButton.FakeDead);

            if (!fakeDeath)
            {
                m_Lifting = true;
                m_PlayerAnimation.RPC_Revive();
                _ = WaitForAnimationEnd(() => { m_ShouldReturn = true; });
                return;
            }
        }

        private void Back2Normal()
        {
            Machine.TryActivateState<PlayerNormalLocomotionState>();
        }
        
        private async UniTask WaitForAnimationEnd(Action a)
        {
            m_CancelSource ??= new CancellationTokenSource();
            float sec = m_PlayerAnimation.CurrentAnimationDuration(0);
            bool cancelled = await UniTask.WaitForSeconds(Mathf.Abs(sec),cancellationToken: m_CancelSource.Token).SuppressCancellationThrow();
            if (cancelled)
            {
                return;
            }
            a?.Invoke();
        }

        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.FakeDead;
        }
    }
}