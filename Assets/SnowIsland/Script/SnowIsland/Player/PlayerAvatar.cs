using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    /// <summary>
    /// 负责自定义玩家的模型而不是动画
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerAvatar: MonoBehaviour
    {
        [ShowInInspector]
        private bool m_RagdollEnable = false;
        private Animator m_Animator;
        private Rigidbody[] m_Rigidbodies;
        private PlayerCharacter m_Character;
        private Transform m_Model;

        private void Awake()
        {
            m_Character = GetComponent<PlayerCharacter>();
            m_Model = m_Character.Model;
            m_Animator = m_Model.GetComponentInChildren<Animator>();
            m_Rigidbodies = m_Model.GetComponentsInChildren<Rigidbody>();
        }

        public void SetModelForward(Vector3 forward)
        {
            m_Model.forward = forward;
        }
        
        public void ToggleRagdoll()
        {
            if (m_RagdollEnable)
            {
                foreach (var rigidbody in m_Rigidbodies)
                {
                    rigidbody.isKinematic = true;
                }
                m_Animator.enabled = true;
                m_RagdollEnable = false;
            }
            else
            {
                foreach (var rigidbody in m_Rigidbodies)
                {
                    rigidbody.isKinematic = false;
                    rigidbody.linearVelocity = Vector3.zero;
                }
                m_Animator.enabled = false;
                m_RagdollEnable = true;
            }
        }

#if UNITY_EDITOR
        [Button]
        public void ToggleRagdollEditor()
        {
            if(Application.isPlaying)
            {
                ToggleRagdoll();
            }
        }
#endif
    }
}