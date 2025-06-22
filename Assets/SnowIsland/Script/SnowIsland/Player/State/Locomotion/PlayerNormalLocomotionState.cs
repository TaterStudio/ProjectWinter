using System;
using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerNormalLocomotionState: PlayerBaseLocomotionState
    {
        private PlayerInputData m_InputData;

        
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Normal;
        }

        protected override void OnFixedUpdate()
        {
            if (GetInput(out m_InputData))
            {
                bool isRun = m_InputData.NetworkButtons.IsSet(PlayerInputButton.Run);
                bool canRun = PlayerState.PlayerStatusData.Food > 0;
                if (isRun && canRun)
                {
                    PlayerState.IsRun = true;
                }
                else
                {
                    PlayerState.IsRun = false;
                }
                PlayerState.MoveDir = m_InputData.MoveDirection;
            }
            else
            {
                PlayerState.MoveDir = Vector3.zero;
            }
        }
    }
}