using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/ItemData/可放置")]
    public class ItemPlaceableDataSO: ItemDataSO
    {
        public int AnimationIndex => m_AnimationIndex;
        [SerializeField]
        private int m_AnimationIndex;
    }
}