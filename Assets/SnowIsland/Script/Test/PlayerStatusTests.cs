using NUnit.Framework;
using TaterGame.SnowIsland.Shared;

//生成By DeepSeek
namespace SnowIsland.Script.GameTest
{
    public class PlayerStatusTests
    {
        [Test]
        public void ApplyFood_ShouldApplyCorrectHealthModifications()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = 50,
                HealthMax = 100,
                Warmth = 50,
                WarmthMax = 100,
                Food = 50,
                FoodMax = 100
            };

            var food = new FoodData
            {
                Health = 10,
                HealthPercent = 10, // 现在表示10%
                MaxHealth = 150,
                MaxHealthPercent = 20 // 现在表示20%
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // Health计算: 50 * (1 + 0.1) + 10 = 65
            // HealthMax计算: 100 * (1 + 0.2) + 10 = 130
            // 差异: 130 - 100 = 30
            // 最终生命值: 65 + 30 = 95
            Assert.AreEqual(95, result.Health);
            Assert.AreEqual(130, result.HealthMax);
        }

        [Test]
        public void ApplyFood_ShouldClampValuesToMax()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = 90,
                HealthMax = 100,
                Warmth = 90,
                WarmthMax = 100,
                Food = 90,
                FoodMax = 100
            };

            var food = new FoodData
            {
                Health = 20,
                HealthPercent = 50, // 50%
                MaxHealth = 110, // 应该限制到这个值
                MaxHealthPercent = 0
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // 无限制时: 90 * 1.5 + 20 = 155
            // 但应该限制到MaxHealth (110)
            Assert.AreEqual(110, result.Health);
            Assert.AreEqual(110, result.HealthMax);
        }

        [Test]
        public void ApplyFood_ShouldHandleZeroValues()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = 0,
                HealthMax = 0,
                Warmth = 0,
                WarmthMax = 0,
                Food = 0,
                FoodMax = 0
            };

            var food = new FoodData
            {
                Health = 10,
                HealthPercent = 10, // 10%
                MaxHealth = 100,
                MaxHealthPercent = 0
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // 生命值: 0 * 1.1 + 10 = 10
            // 最大生命值: 0 * 1 + 10 = 10
            // 差异: 10 - 0 = 10
            // 最终生命值: 10 + 10 = 20
            Assert.AreEqual(20, result.Health);
            Assert.AreEqual(10, result.HealthMax);
        }

        [Test]
        public void ApplyFood_ShouldHandleNegativeValues()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = -10,
                HealthMax = 100,
                Warmth = -10,
                WarmthMax = 100,
                Food = -10,
                FoodMax = 100
            };

            var food = new FoodData
            {
                Health = -5,
                HealthPercent = -10, // -10%
                MaxHealth = 100,
                MaxHealthPercent = -10 // -10%
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // 应该将负值限制为0
            Assert.AreEqual(0, result.Health);
            // 最大生命值: 100 * 0.9 + (-5) = 85
            Assert.AreEqual(85, result.HealthMax);
        }

        [Test]
        public void ApplyFood_ShouldApplyAllStatModifications()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = 50,
                HealthMax = 100,
                Warmth = 60,
                WarmthMax = 120,
                Food = 70,
                FoodMax = 140
            };

            var food = new FoodData
            {
                Health = 5,
                HealthPercent = 10,
                MaxHealth = 110,
                MaxHealthPercent = 10,

                Warmth = 10,
                WarmthPercent = 20,
                MaxWarmth = 130,
                MaxWarmthPercent = 20,

                Food = 15,
                FoodPercent = 30,
                MaxFood = 150,
                MaxFoodPercent = 30
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // 生命值计算:
            // 基础: 50 * 1.1 + 5 = 60
            // 最大: 100 * 1.1 + 5 = 115 (限制到110)
            // 差异: 110 - 100 = 10
            // 最终生命值: 60 + 10 = 70
            Assert.AreEqual(70, result.Health);
            Assert.AreEqual(110, result.HealthMax);

            // 温暖值计算:
            // 基础: 60 * 1.2 + 10 = 82
            // 最大: 120 * 1.2 + 10 = 154 (限制到130)
            // 差异: 130 - 120 = 10
            // 最终温暖值: 82 + 10 = 92
            Assert.AreEqual(92, result.Warmth);
            Assert.AreEqual(130, result.WarmthMax);

            // 食物值计算:
            // 基础: 70 * 1.3 + 15 = 106
            // 最大: 140 * 1.3 + 15 = 197 (限制到150)
            // 差异: 150 - 140 = 10
            // 最终食物值: 106 + 10 = 116
            Assert.AreEqual(116, result.Food);
            Assert.AreEqual(150, result.FoodMax);
        }

        [Test]
        public void ApplyFood_ShouldNotExceedAbsoluteMaximums()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = 9000,
                HealthMax = 10000,
                Warmth = 9000,
                WarmthMax = 10000,
                Food = 9000,
                FoodMax = 10000
            };

            var food = new FoodData
            {
                Health = 2000,
                HealthPercent = 50,
                MaxHealth = 20000,
                MaxHealthPercent = 50
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // 应该限制到绝对最大值10000
            Assert.AreEqual(10000, result.Health);
            Assert.AreEqual(10000, result.HealthMax);
        }

        [Test]
        public void ApplyFood_ShouldCorrectlyCalculateMaxDifferences()
        {
            // Arrange
            var player = new PlayerStatusData
            {
                Health = 80,
                HealthMax = 100,
                Warmth = 80,
                WarmthMax = 100,
                Food = 80,
                FoodMax = 100
            };

            var food = new FoodData
            {
                Health = 5,
                HealthPercent = 10,
                MaxHealth = 120,
                MaxHealthPercent = 10,

                Warmth = 5,
                WarmthPercent = 10,
                MaxWarmth = 120,
                MaxWarmthPercent = 10,

                Food = 5,
                FoodPercent = 10,
                MaxFood = 120,
                MaxFoodPercent = 10
            };

            // Act
            var result = FoodCalculator.ApplyFood(player, food);

            // Assert
            // 检查所有属性的差异计算是否正确
            Assert.AreEqual(120, result.HealthMax);
            Assert.AreEqual(120, result.WarmthMax);
            Assert.AreEqual(120, result.FoodMax);

            // 检查最终值是否应用了正确的差异
            // 生命值: 80 * 1.1 + 5 = 93 + (120-100=20) = 113
            Assert.AreEqual(113, result.Health);
            Assert.AreEqual(113, result.Warmth);
            Assert.AreEqual(113, result.Food);
        }
    }
}