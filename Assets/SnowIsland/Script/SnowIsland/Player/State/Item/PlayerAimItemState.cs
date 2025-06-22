using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerAimItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Aim;
        }
    }
}