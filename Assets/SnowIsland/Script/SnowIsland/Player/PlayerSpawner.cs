using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerSpawner: SimulationBehaviour, IPlayerJoined
    {
        public GameObject PlayerPrefab;
        private EventCenter m_EventCenter;

        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
        }

        public void PlayerJoined(PlayerRef player)
        {
            if (player == Runner.LocalPlayer)
            {
                var obj = Runner.Spawn(PlayerPrefab).gameObject;
                if (obj.TryGetComponent(out PlayerCharacter playerCharacter))
                {
                    m_EventCenter.PlayerSpawnEvent.OnNext(new PlayerSpawnEvent()
                        { PlayerObject = playerCharacter.gameObject, HeadPivot = playerCharacter.HeadPivot });
                }
            }
        }
    }
}