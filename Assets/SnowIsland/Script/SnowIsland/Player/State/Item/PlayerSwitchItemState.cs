using System;
using Fusion.Addons.FSM;

namespace TaterGame.SnowIsland
{
    public class PlayerSwitchItemState: PlayerBaseItemState
    {
        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
            if (Machine.StateTime >= 0.5f)
            {
                Machine.TryActivateState<PlayerIdleItemState>();
            }
        }

        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Switch;
        }
    }
}