using System;
using Fusion;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    public class Interactable: NetworkBehaviour
    {
        [Networked]
        public bool IsOccupied { get; set; }

        public Vector3 InteractHudOffset = new Vector3(0, 2, 0);

        public InteractItemSO[] Interaction;
    
        private ClientState m_ClientState;
        private EventCenter m_EventCenter;
        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_ClientState = ApplicationScope.Resolve<ClientState>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != m_ClientState.LocalPlayer)
            {
                return;
            }
            Debug.Log($"本地玩家靠近了{gameObject.name}");
            m_EventCenter.OnPlayerEnterInteract.OnNext(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject != m_ClientState.LocalPlayer)
            {
                return;
            }
            Debug.Log($"本地玩家离开了{gameObject.name}");
            m_EventCenter.OnPlayerExitInteract.OnNext(this);
        }

        public void OnInteractSuccess(GameObject playerObj,int index)
        {
            this.Interaction[index].CompleteInteract(playerObj, gameObject);
        }
        
        public void OnInteractFail(GameObject playerObj,int index)
        {
            this.Interaction[index].CancelInteract(playerObj, gameObject);
        }
    }
}