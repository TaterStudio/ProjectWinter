using Fusion.Addons.FSM;

namespace TaterGame.SnowIsland
{
    public abstract class PlayerBaseItemState: StateBehaviour
    {
        private PlayerState m_PlayerState;

        private void Awake()
        {
            m_PlayerState = GetComponentInParent<PlayerState>();
        }

        protected override void OnEnterState()
        {
            m_PlayerState.ItemState = GetLocomotionState();
        }

        protected abstract PlayerItemState GetLocomotionState();
    }
}