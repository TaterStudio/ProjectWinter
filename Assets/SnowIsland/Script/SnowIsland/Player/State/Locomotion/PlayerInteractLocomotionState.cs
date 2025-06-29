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
        private EventCenter m_EventCenter;
        private bool m_ShouldTransit;
        /// <summary>
        /// 是否是成功完成的交互，
        /// 如果是动画播放完成的交互，则返回true
        /// 其他情况则为false
        /// </summary>
        private bool m_IsSuccess = false;

        protected override void Awake()
        {
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_PlayerAnimation.OnExitInteract.Subscribe(x =>
            {
                m_ShouldTransit = true;
            });
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            base.Awake();
        }

        protected override void OnEnterState()
        {
            m_ShouldTransit = false;
            m_PlayerAnimation.RPC_Interact(m_CurrentInteraction.AnimationIndex);
            base.OnEnterState();
            m_IsSuccess = false;
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (m_ShouldTransit)
            {
                m_IsSuccess = true;
                m_ShouldTransit = false;
                Machine.TryActivateState<PlayerNormalLocomotionState>();
            }
        }

        protected override void OnExitState()
        {
            m_PlayerAnimation.RPC_PlayMove();
            base.OnExitState();
            m_EventCenter.PlayerInteractSuccess.OnNext(new PlayerEndInteractEvent()
            {
                Sender = m_PlayerAnimation.gameObject,
                Success = m_IsSuccess
            });
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