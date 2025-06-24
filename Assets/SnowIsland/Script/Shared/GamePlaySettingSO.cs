using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "TaterGame/SnowIsland/GamePlaySetting", order = 0)]
    public class GamePlaySettingSO: ScriptableObject
    {
        public IngamePlayerSetting IngamePlayerSetting;
    }

    [Serializable]
    public class IngamePlayerSetting
    {
        public int WarmthGainPerSecond = -2;
        public int FoodGainPerSecond = -1;
        public int FoodGainRunning = -2;
        public int WarmthGainRunning = 1;
        public int HealthNaturalRecover = 5;
        public int HealthNaturalRecoverThresholdIn100 = 60;
        public int HealthNaturalRecoverFoodGain = -1;
        public float TickFrequency = 10;
        public Material GhostMaterial;
    }
}