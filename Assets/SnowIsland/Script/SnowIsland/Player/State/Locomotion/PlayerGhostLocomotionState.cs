namespace TaterGame.SnowIsland
{
    public class PlayerGhostLocomotionState: PlayerBaseLocomotionState
    {
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Ghost;
        }
    }
}