using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerCharacter: NetworkBehaviour
    {
        public GameObject HeadPivot => m_HeadPivot;
        [SerializeField]
        private GameObject m_HeadPivot;

        public Transform Model=>m_Model;
        [SerializeField]
        private Transform m_Model;

        public Action OnSpawned;
        
        public override void Spawned()
        {
            OnSpawned?.Invoke();
        }
    }
}