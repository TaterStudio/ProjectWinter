using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerInteractLocomotionState: PlayerBaseLocomotionState
    {
        private InteractItemSO m_CurrentInteraction;
        private PlayerAnimation m_PlayerAnimation;
        private float m_Duration;
        private bool m_ShouldTransit;

        protected override void Awake()
        {
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_PlayerAnimation.OnExitInteract.Subscribe(x =>
            {
                m_ShouldTransit = true;
            });
            base.Awake();
        }

        protected override void OnEnterState()
        {
            m_ShouldTransit = false;
            m_PlayerAnimation.RPC_Interact(m_CurrentInteraction.AnimationIndex);
            base.OnEnterState();
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (m_ShouldTransit)
            {
                Machine.TryActivateState<PlayerNormalLocomotionState>();
                m_ShouldTransit = false;
            }
        }

        protected override void OnExitState()
        {
            m_PlayerAnimation.RPC_PlayMove();
            base.OnExitState();
        }

        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Interact;
        }

        public void SetInteraction(InteractItemSO interactItemSo)
        {
            m_CurrentInteraction = interactItemSo;
        }
    }
}