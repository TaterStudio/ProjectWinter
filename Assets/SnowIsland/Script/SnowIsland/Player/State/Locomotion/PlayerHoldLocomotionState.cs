namespace TaterGame.SnowIsland
{
    public class PlayerHoldLocomotionState: PlayerBaseLocomotionState
    {
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Hold;
        }
    }
}