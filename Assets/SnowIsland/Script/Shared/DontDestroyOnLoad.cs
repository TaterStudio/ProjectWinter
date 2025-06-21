using System;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    public class DontDestroyOnLoad: MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}