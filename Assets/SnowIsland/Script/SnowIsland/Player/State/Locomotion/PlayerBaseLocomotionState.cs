using Fusion.Addons.FSM;

namespace TaterGame.SnowIsland
{
    public abstract class PlayerBaseLocomotionState: StateBehaviour
    {
        private PlayerState m_PlayerState;

        private void Awake()
        {
            m_PlayerState = GetComponentInParent<PlayerState>();
        }

        protected override void OnEnterState()
        {
            m_PlayerState.LocomotionState = GetLocomotionState();
        }

        protected abstract PlayerLocomotionState GetLocomotionState();
    }
}