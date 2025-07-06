using System;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    public class IndicatorCenter: MonoBehaviour
    {
        private EventCenter m_EventCenter;
        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
        }

        private void Start()
        {
            m_EventCenter.SetIndicatorCenter.OnNext(this);
        }
    }
}