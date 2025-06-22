using System;
using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerNormalLocomotionState: PlayerBaseLocomotionState
    {
        private PlayerStateMachine m_MachineRoot;
        private PlayerAnimation m_PlayerAnimation;

        protected override void Awake()
        {
            base.Awake();
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_MachineRoot = GetComponentInParent<PlayerStateMachine>();
        }
        
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Normal;
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            bool fakeDeath = m_InputData.NetworkButtons.IsSet(PlayerInputButton.FakeDead);

            if (fakeDeath)
            {
                Machine.TryActivateState<PlayerFakeDeadLocomotionState>();
                return;
            }
            
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
            
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            m_PlayerAnimation.RPC_PlayMove();
        }

        protected override void OnExitState()
        {
            m_MachineRoot.MarkItemIdle();
        }
    }
}