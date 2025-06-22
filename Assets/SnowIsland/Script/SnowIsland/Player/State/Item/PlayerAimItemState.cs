namespace TaterGame.SnowIsland
{
    public class PlayerAimItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Aim;
        }
    }
}