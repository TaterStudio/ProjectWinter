using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "TaterGame/SnowIsland/InteractionTable", order = 0)]
    public class InteractionTableSO: ScriptableObject
    {
        [SerializeField]
        private InteractItemSO m_DroppedItemPickUp;
        public InteractItemSO DroppedItemPickUp => m_DroppedItemPickUp;
    }
}