using System;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class SnowCamera: MonoBehaviour
    {
        [SerializeField]
        private BF_InteractiveEffects m_InteractiveEffects;

        private EventCenter m_EventCenter;
        private void Start()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.PlayerSpawnEvent.Subscribe(OnPlayerSpawn).AddTo(this);
        }
        private void OnPlayerSpawn(PlayerSpawnEvent e)
        {
            m_InteractiveEffects.transformToFollow = e.HeadPivot.transform;
        }
    }
}