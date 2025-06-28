using HeathenEngineering.Events;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "TaterGame/SnowIsland/CommandTable", order = 0)]
    public class CommandTableSO: ScriptableObject
    {
        public GameEvent ListItemCommand;
        public GameEvent<string> DropItemCommand;
    }
}