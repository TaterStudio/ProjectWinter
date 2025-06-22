using System;
using Fusion;

namespace TaterGame.SnowIsland.Shared
{
    [Serializable]
    public struct PlayerStatusData: INetworkStruct
    {
        public int Health;

        public int HealthMax;

        public int Warmth;

        public int WarmthMax;

        public int Food;

        public int FoodMax;
    }
}