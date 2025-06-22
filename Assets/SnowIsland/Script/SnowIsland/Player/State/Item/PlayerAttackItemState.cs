using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerAttackItemState: PlayerBaseItemState
    {
        private PlayerAnimation m_PlayerAnimation;
        private PlayerIdleItemState m_PlayerIdleItemState;
        public float Duration { get; set; }

        protected override void Awake()
        {
            base.Awake();
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_PlayerIdleItemState = GetStateComponent<PlayerIdleItemState>();
        }

        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Attack;
        }

        protected override void OnEnterState()
        {
            m_PlayerAnimation.RPC_PlayAtkRpc();
        }

        protected override void OnExitState()
        {
            m_PlayerAnimation.RPC_ExitAtkAnimRpc();
        }

        protected override void OnFixedUpdate()
        {
            if (Machine.StateTime > Duration)
            {
                Machine.TryActivateState(m_PlayerIdleItemState.StateId);
            }
        }

    }
}