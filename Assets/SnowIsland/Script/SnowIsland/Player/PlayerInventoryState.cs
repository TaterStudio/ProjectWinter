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

        private ItemService m_ItemService;
        private EventCenter m_EventCenter;
        private void Awake()
        {
            m_ItemService = ApplicationScope.Resolve<ItemService>();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            
        }

        public bool CanSlotContainItem(ItemInfo slot,string itemId)
        {
            if (!m_ItemService.TryGetItem(slot.ItemId,out var item))
            {
                return true;
            }
            if (itemId.GetHashCode() != slot.ItemId)
            {
                return false;
            }
            return item.StackSize > slot.Amount;
        }

        public int GetFirstContainableSlot(string id)
        {
            if (CanSlotContainItem(InvSlot1, id))
            {
                return 0;
            }
            if (CanSlotContainItem(InvSlot2, id))
            {
                return 1;
            }

            if (CanSlotContainItem(InvSlot3, id))
            {
                return 2;
            }
            if (CanSlotContainItem(InvSlot4, id))
            {
                return 3;
            }
            if (InvSlot5Unlocked&& CanSlotContainItem(InvSlot5, id))
            {
                return 4;
            }
            return -1;
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