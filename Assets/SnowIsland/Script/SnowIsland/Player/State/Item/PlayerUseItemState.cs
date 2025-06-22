using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerUseItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Use;
        }
    }
}