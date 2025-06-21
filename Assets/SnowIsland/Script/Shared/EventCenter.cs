using System;
using UniRx;

namespace TaterGame.SnowIsland.Shared
{
    public class EventCenter
    {
        public Subject<PlayerSpawnEvent> PlayerSpawnEvent => m_PlayerSpawnEvent;
        private Subject<PlayerSpawnEvent> m_PlayerSpawnEvent = new Subject<PlayerSpawnEvent>();

        public Subject<PlayerStatusEvent> PlayerHealthEvent => m_PlayerHealthEvent;
        private Subject<PlayerStatusEvent> m_PlayerHealthEvent = new();

        public Subject<PlayerStatusEvent> PlayerWarmthEvent => m_PlayerWarmthEvent;
        private Subject<PlayerStatusEvent> m_PlayerWarmthEvent = new();
        
        public Subject<PlayerStatusEvent> PlayerFoodEvent => m_PlayerFoodEvent;
        private Subject<PlayerStatusEvent> m_PlayerFoodEvent = new();
    }
}