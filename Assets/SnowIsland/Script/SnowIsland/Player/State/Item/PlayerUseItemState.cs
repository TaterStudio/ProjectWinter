namespace TaterGame.SnowIsland
{
    public class PlayerUseItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Use;
        }
    }
}