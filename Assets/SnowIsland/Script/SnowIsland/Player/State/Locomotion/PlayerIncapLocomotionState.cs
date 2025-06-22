namespace TaterGame.SnowIsland
{
    public class PlayerIncapLocomotionState: PlayerBaseLocomotionState
    {
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Incap;
        }
    }
}