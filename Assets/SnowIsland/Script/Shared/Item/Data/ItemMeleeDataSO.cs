using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/ItemData/近战武器")]
    public class ItemMeleeDataSO: ItemDataSO, IUsableItem
    {
        [SerializeField]
        private float m_UseDuration = 1;
        public float UseDuration { get => m_UseDuration; }

        /// <summary>
        /// 武器的动画是单独的
        /// </summary>
        public int AnimIndex { get=>-1; }
    }
}