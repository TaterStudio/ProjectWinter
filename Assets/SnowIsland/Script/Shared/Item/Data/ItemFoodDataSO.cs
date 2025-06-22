using System;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/ItemData/Food")]
    public class ItemFoodDataSO: ItemDataSO
    {
        [SerializeField]
        private FoodData m_FoodData;

        public FoodData FoodData
        {
            get { return m_FoodData; }
        }
    }

    [Serializable]
    public struct FoodData
    {
        public int Health;
        public int Warmth;
        public int Food;
        public int HealthPercent;
        public int WarmthPercent;
        public int FoodPercent;
        public int MaxHealth;
        public int MaxWarmth;
        public int MaxFood;
        public int MaxHealthPercent;
        public int MaxWarmthPercent;
        public int MaxFoodPercent;
    }
}