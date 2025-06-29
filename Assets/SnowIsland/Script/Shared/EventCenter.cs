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

        public Subject<string> Log2Console => m_Log2Console;
        private Subject<string> m_Log2Console = new();

        public Subject<Interactable> OnPlayerEnterInteract => m_OnPlayerEnterInteract;
        private Subject<Interactable> m_OnPlayerEnterInteract = new();

        public Subject<Interactable> OnPlayerExitInteract => m_OnPlayerExitInteract;
        private Subject<Interactable> m_OnPlayerExitInteract = new();

        public Subject<Interactable> PlayerCurrentInteractable => m_PlayerCurrentInteractable;
        private Subject<Interactable> m_PlayerCurrentInteractable = new();

        public Subject<PlayerEndInteractEvent> PlayerInteractSuccess => m_PlayerInteractSuccess;
        private Subject<PlayerEndInteractEvent> m_PlayerInteractSuccess = new();
    }
}