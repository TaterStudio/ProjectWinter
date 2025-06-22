using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/ItemData/近战武器")]
    public class ItemMeleeDataSO: ItemDataSO, IMeleeItem
    {
        [SerializeField]
        private float m_UseDuration = 1;
        public float UseDuration { get => m_UseDuration; }
    }
}