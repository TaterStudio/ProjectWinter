using System;
using UniRx;
using UnityEngine;

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

        public Subject<GameObject> OnInteractableDestroyed => m_OnInteractableDestroyed;
        private Subject<GameObject> m_OnInteractableDestroyed = new();
        
        private Subject<int> m_OnInventoryIndexChange = new();
        public Subject<int> OnInventoryIndexChange => m_OnInventoryIndexChange;
        private Subject<PlayerItemSlotChangeEvent> m_OnSlotChange = new();
        public Subject<PlayerItemSlotChangeEvent> OnSlotChange => m_OnSlotChange;
        private Subject<bool> m_OnSlot5Unlock = new();
        public Subject<bool> OnSlot5Unlock => m_OnSlot5Unlock;
        
        public Subject<ItemDropEvent> ItemDropEvent => m_ItemDropEvent;
        private Subject<ItemDropEvent> m_ItemDropEvent = new();
    }
}