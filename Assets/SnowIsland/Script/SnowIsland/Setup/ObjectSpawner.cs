using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class ObjectSpawner: SimulationBehaviour, IPlayerJoined
    {
        public GameObject PlayerPrefab;
        private EventCenter m_EventCenter;
        private ClientState m_ClientState;

        private void Start()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_ClientState = ApplicationScope.Resolve<ClientState>();
        }

        public void PlayerJoined(PlayerRef player)
        {
            if (player == Runner.LocalPlayer)
            {
                var networkObject = Runner.Spawn(PlayerPrefab);
                networkObject.AssignInputAuthority(Runner.LocalPlayer);
                var obj = networkObject.gameObject;
                m_ClientState.LocalPlayer = obj;
                m_ClientState.LocalRunner = Runner;
                if (obj.TryGetComponent(out PlayerCharacter playerCharacter))
                {
                    m_EventCenter.PlayerSpawnEvent.OnNext(new PlayerSpawnEvent()
                        { PlayerObject = playerCharacter.gameObject, HeadPivot = playerCharacter.HeadPivot });
                }
            }
        }
    }
}