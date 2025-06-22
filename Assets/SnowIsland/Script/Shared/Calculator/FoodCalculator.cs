using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    public static class FoodCalculator
    {
        public static PlayerStatusData ApplyFood(PlayerStatusData playerStatusData, FoodData foodData)
        {
            int health = Mathf.Clamp(playerStatusData.Health * (1 + (foodData.HealthPercent/100) ) + foodData.Health, 0, foodData.MaxHealth);
            int warmth = Mathf.Clamp(playerStatusData.Warmth * (1 + (foodData.WarmthPercent/100)) + foodData.Warmth, 0, foodData.MaxWarmth);
            int food = Mathf.Clamp(playerStatusData.Food * (1 + (foodData.FoodPercent/100)) + foodData.Food, 0, foodData.MaxFood);
            int healthMax = Mathf.Clamp(playerStatusData.HealthMax * (1 + (foodData.MaxHealthPercent/100)) + foodData.Health, 0, 10000);
            int warmthMax = Mathf.Clamp(playerStatusData.WarmthMax * (1 + (foodData.MaxWarmthPercent/100)) + foodData.Warmth, 0,10000);
            int foodMax = Mathf.Clamp(playerStatusData.FoodMax * (1 + (foodData.MaxFoodPercent/100)) + foodData.Food, 0,10000);
            int maxHealthMaxDiff = healthMax = playerStatusData.HealthMax;
            int maxWarmthMaxDiff = warmthMax = playerStatusData.WarmthMax;
            int maxFoodMaxDiff = foodMax = playerStatusData.FoodMax;
            health += maxHealthMaxDiff;
            warmth += maxWarmthMaxDiff;
            food += maxFoodMaxDiff;
            
            return new PlayerStatusData
            {
                Food = food,
                Warmth = warmth,
                Health = health,
                FoodMax = foodMax,
                WarmthMax = warmthMax,
                HealthMax = healthMax
            };
        }
    }
}