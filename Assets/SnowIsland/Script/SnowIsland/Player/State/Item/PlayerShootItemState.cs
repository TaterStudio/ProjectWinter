using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerShootItemState: PlayerBaseItemState
    {
        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Shoot;
        }
    }
}