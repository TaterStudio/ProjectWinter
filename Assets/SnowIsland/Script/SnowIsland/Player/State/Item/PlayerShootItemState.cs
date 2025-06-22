namespace TaterGame.SnowIsland
{
    public class PlayerShootItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Shoot;
        }
    }
}