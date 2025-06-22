using System;
using HeathenEngineering.Events;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerCommand: MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimationEvent m_ChangeAnimationCommand;
        [SerializeField]
        private GameEvent m_ToggleRagdollCommand;

        private PlayerAvatar m_PlayerAvatar;
        private void Awake()
        {
            m_PlayerAvatar = GetComponent<PlayerAvatar>();
            m_ToggleRagdollCommand.AddListener(ToggleRagdoll);
            m_ChangeAnimationCommand.AddListener(ChangeAnimation);
        }

        private void OnDestroy()
        {
            m_ToggleRagdollCommand.RemoveListener(ToggleRagdoll);
            m_ChangeAnimationCommand.RemoveListener(ChangeAnimation);
        }

        private void ChangeAnimation(EventData<PlayerAnimationEventData> arg0)
        {
            
        }

        private void ToggleRagdoll(EventData arg0)
        {
            m_PlayerAvatar.ToggleRagdoll();
        }
    }
}