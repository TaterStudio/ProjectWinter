using Fusion.Addons.FSM;

namespace TaterGame.SnowIsland
{
    public abstract class PlayerBaseItemState: StateBehaviour
    {
        protected PlayerState PlayerState { get; private set; }

        private void Awake()
        {
            PlayerState = GetComponentInParent<PlayerState>();
        }

        protected override void OnEnterState()
        {
            PlayerState ??= GetComponentInParent<PlayerState>();
            PlayerState.ItemState = GetLocomotionState();
        }

        protected abstract PlayerItemState GetLocomotionState();
    }
}