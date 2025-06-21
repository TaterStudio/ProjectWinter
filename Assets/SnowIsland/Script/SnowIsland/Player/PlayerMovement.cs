using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaterGame.SnowIsland
{
    public class PlayerMovement: NetworkBehaviour
    {
        public const int SpeedScale = 100;
        private PlayerInput m_PlayerInput;
        private CharacterController m_CharacterController;
        private PlayerAvatar m_PlayerAvatar;
        private int m_RunSpeed = 400;
        private int m_WalkSpeed = 400;
        private void Start()
        {
            m_PlayerInput = ApplicationScope.Resolve<PlayerInput>();
            m_CharacterController = GetComponent<CharacterController>();
            m_PlayerAvatar = GetComponent<PlayerAvatar>();
        }

        public override void FixedUpdateNetwork()
        {
            if (m_PlayerInput == null)
            {
                return;
            }
            var direction = m_PlayerInput.actions[Constraints.Input.Move].ReadValue<Vector2>();
            float actualSpeed = (float)m_WalkSpeed / SpeedScale;
            Vector3 deltaMove = new Vector3(direction.x,0,direction.y)* actualSpeed * Runner.DeltaTime;
            m_CharacterController.Move(deltaMove);
            if(deltaMove!=Vector3.zero){
                m_PlayerAvatar.SetModelForward(deltaMove);
            }
        }
    }
}