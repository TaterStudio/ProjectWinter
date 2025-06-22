using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/ItemData/近战武器")]
    public class ItemMeleeDataSO: ItemDataSO, IMeleeItem
    {

        [SerializeField]
        private float m_AtkZOffset = 1f;

        public float AtkZOffset
        {
            get => m_AtkZOffset;
        }

        [SerializeField]
        private float m_AtkSphereRadius = 0.5f;

        public float AtkSphereRadius
        {
            get => m_AtkSphereRadius;
        }

        [SerializeField]
        private int m_AtkDamage = 80;

        public int AtkDamage
        {
            get => m_AtkDamage;
        }
    }
}