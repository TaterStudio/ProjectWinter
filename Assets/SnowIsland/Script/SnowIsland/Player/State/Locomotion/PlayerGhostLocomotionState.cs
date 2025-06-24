using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerGhostLocomotionState: PlayerBaseLocomotionState
    { 
        private PlayerCharacter m_PlayerCharacter;
        protected override void Awake()
        {
            base.Awake();
            m_PlayerCharacter = GetComponentInParent<PlayerCharacter>();
        }

        protected override void OnEnterState()
        {
            base.OnEnterState();
            m_PlayerCharacter.RPC_GhostMode();
        }

        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Ghost;
        }
    }
}