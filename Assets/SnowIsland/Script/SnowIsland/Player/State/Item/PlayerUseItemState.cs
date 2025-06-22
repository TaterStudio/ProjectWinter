using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerUseItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Use;
        }
    }
}