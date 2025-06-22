using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerInventoryState: NetworkBehaviour
    {
        
        [Networked]
        public RadioType RadioType{ get; set; }

        [Networked]
        public ItemInfo InvSlot1{ get; set; }

        [Networked]
        public ItemInfo InvSlot2{ get; set; }

        [Networked]
        public ItemInfo InvSlot3{ get; set; }

        [Networked]
        public ItemInfo InvSlot4{ get; set; }

        [Networked]
        public ItemInfo InvSlot5{ get; set; }
        
        [Networked]
        public NetworkBool InvSlot5Unlocked{ get; set; }
        
        [Networked]
        public int InventorySlotIndex { get; set; }
        
        public ItemInfo CurrentItemInfo
        {
            get
            {
                switch (InventorySlotIndex)
                {
                    case 0:
                        return InvSlot1;
                    case 1:
                        return InvSlot2;
                    case 2:
                        return InvSlot3;
                    case 3:
                        return InvSlot4;
                    case 4:
                        return InvSlot5;
                    default:
                        throw new NullReferenceException($"非法的背包Index{InventorySlotIndex}");
                }
            }
        }

        
        [Serializable]
        public struct ItemInfo: INetworkStruct
        {
            public int ItemId;
            public int Amount;
            public int Metadata;
            public int Skin;
        }
    }
}