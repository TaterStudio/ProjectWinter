namespace TaterGame.SnowIsland
{
    public class PlayerInteractLocomotionState: PlayerBaseLocomotionState
    {
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Interact;
        }
    }
}