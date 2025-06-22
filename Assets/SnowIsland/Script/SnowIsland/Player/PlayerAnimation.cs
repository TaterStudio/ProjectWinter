using System;
using Animancer;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerAnimation: MonoBehaviour
    {
        [SerializeField]
        private TransitionAsset m_Dash;
        
        [SerializeField]
        private TransitionAsset m_Death;

        [SerializeField]
        private TransitionAsset m_Revive;

        [SerializeField]
        private TransitionAsset m_Movement;

        [SerializeField]
        private TransitionAsset m_MovementAim;

        [SerializeField]
        private TransitionAsset m_Atk;

        private PlayerCharacter m_PlayerCharacter;
        private AnimancerComponent m_AnimancerComponent;
        private PlayerState m_PlayerState;

        private PlayerMovement m_PlayerMovement;

        /// <summary>
        /// 攻击有三种，显示具体攻击是哪一种
        /// </summary>
        [ShowInInspector,ReadOnly]
        private int m_AtkIndex;
        
        private void Awake()
        {
            m_PlayerCharacter = GetComponent<PlayerCharacter>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerState = GetComponent<PlayerState>();
            m_AnimancerComponent = m_PlayerCharacter.Model.GetComponent<AnimancerComponent>();
            m_PlayerMovement.OnVelocityChange += SetVelocity;
        }

        
        private void SetVelocity(int obj)
        {
            m_AnimancerComponent.Parameters.SetValue("Speed",(float)m_PlayerMovement.Velocity / Constraints.Character.SpeedScale);
        }

        private void Start()
        {
            m_AnimancerComponent.Play(m_Movement);
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }
    }
}