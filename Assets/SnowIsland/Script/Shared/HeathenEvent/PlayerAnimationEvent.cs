using HeathenEngineering.Events;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(menuName = "TaterGame/SnowIsland/HeathenEvents/PlayerAnimationEvent")]
    public class PlayerAnimationEvent: GameEvent<PlayerAnimationEventData>
    {
        
    }

    public struct PlayerAnimationEventData
    {
        public string State;
        public float Parameter1;
    }
}