using System;
using System.Runtime.CompilerServices;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerMovement: NetworkBehaviour
    { 
        [Networked,OnChangedRender(nameof(InvokeOnVelocityChange))]
        public int Velocity { get; set; }
        public Action<int> OnVelocityChange;
        private CharacterController m_CharacterController;
        private PlayerAvatar m_PlayerAvatar;
        [SerializeField]
        private int m_RunSpeed = 600;
        [SerializeField]
        private int m_WalkSpeed = 400;

        private PlayerInputData m_InputData;

        private PlayerState m_PlayerState;

        private void Awake()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_PlayerAvatar = GetComponent<PlayerAvatar>();
            m_PlayerState = GetComponent<PlayerState>();
        }

        private void Update()
        {
        }

        public override void FixedUpdateNetwork()
        {
            if (!m_PlayerState.CanMove())
            {
                return;
            }
            
            if (!GetInput<PlayerInputData>(out m_InputData))
            {
                return;
            }
            
            int finalSpeed = m_WalkSpeed;
            if (CanRun())
            {
                finalSpeed = m_RunSpeed; 
            } 
            var direction = m_InputData.MoveDirection;
            float actualSpeed = (float)finalSpeed / Constraints.Character.SpeedScale;
            Vector3 deltaMove = new Vector3(direction.x,0,direction.y)* actualSpeed * Runner.DeltaTime;
            m_CharacterController.Move(deltaMove);
            if(deltaMove!=Vector3.zero){
                m_PlayerAvatar.SetModelForward(deltaMove);
            }
            else
            {
                finalSpeed = 0;
            }

            Velocity = finalSpeed;
        }


        private void InvokeOnVelocityChange()
        {
            OnVelocityChange?.Invoke(this.Velocity);
        }

        /// <summary>
        /// 玩家是否可以奔跑
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CanRun()
        {
            return m_PlayerState.IsRun;
        }
    }
}