using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public abstract class PlayerBaseLocomotionState: StateBehaviour
    {
        protected PlayerInputData m_InputData;
        protected bool HasInput { get; private set; }
        protected PlayerState PlayerState { get; private set; }

        protected virtual void Awake()
        {
            PlayerState = GetComponentInParent<PlayerState>();
        }

        protected override void OnEnterState()
        {
            PlayerState.LocomotionState = GetLocomotionState();
        }

        protected override void OnFixedUpdate()
        {
            if (PlayerState == null)
            {
                return;
            }
            
            HasInput = GetInput(out m_InputData);
            if (HasInput)
            {
                PlayerState.MoveDir = m_InputData.MoveDirection;
            }
            else
            {
                PlayerState.MoveDir = Vector3.zero;
            }
        }

        protected abstract PlayerLocomotionState GetLocomotionState();
    }
}