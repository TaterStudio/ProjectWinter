using System;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerCharacter: MonoBehaviour
    {
        public GameObject HeadPivot => m_HeadPivot;
        [SerializeField]
        private GameObject m_HeadPivot;
        
        private void Start()
        {
        }
    }
}