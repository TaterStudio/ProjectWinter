using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerShootItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Shoot;
        }
    }
}