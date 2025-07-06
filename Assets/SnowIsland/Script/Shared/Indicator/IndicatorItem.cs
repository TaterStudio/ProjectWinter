using System;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    public class IndicatorItem: MonoBehaviour
    {
        public Sprite IndicatorTexture => m_IndicatorTexture;
        [SerializeField]
        private Sprite m_IndicatorTexture;
        public Vector3 m_Offset;
        public bool m_ShowOutbounds;
        private EventCenter m_EventCenter;
        public Vector3 IndicatorPosition { get=> transform.position + m_Offset; }

        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
        }

        private void Start()
        {
            m_EventCenter.OnIndicatorItemSpawn.OnNext(this);
        }

        private void OnDestroy()
        {
            m_EventCenter.OnIndicatorItemDestroyed.OnNext(this);
        }
    }
}