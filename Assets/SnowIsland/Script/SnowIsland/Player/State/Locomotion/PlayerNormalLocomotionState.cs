using System;
using Fusion.Addons.FSM;

namespace TaterGame.SnowIsland
{
    public class PlayerNormalLocomotionState: PlayerBaseLocomotionState
    {
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Normal;
        }
    }
}