using Fusion.Addons.FSM;

namespace TaterGame.SnowIsland
{
    public abstract class PlayerBaseLocomotionState: StateBehaviour
    {
        protected PlayerState PlayerState { get; private set; }

        private void Awake()
        {
            PlayerState = GetComponentInParent<PlayerState>();
        }

        protected override void OnEnterState()
        {
            PlayerState.LocomotionState = GetLocomotionState();
        }

        protected abstract PlayerLocomotionState GetLocomotionState();
    }
}