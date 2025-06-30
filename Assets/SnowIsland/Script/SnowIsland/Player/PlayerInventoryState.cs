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

        [Networked,OnChangedRender(nameof(OnSlot1Changed))]
        public ItemInfo InvSlot1{ get; set; }

        [Networked,OnChangedRender(nameof(OnSlot2Changed))]
        public ItemInfo InvSlot2{ get; set; }

        [Networked,OnChangedRender(nameof(OnSlot3Changed))]
        public ItemInfo InvSlot3{ get; set; }

        [Networked,OnChangedRender(nameof(OnSlot4Changed))]
        public ItemInfo InvSlot4{ get; set; }

        [Networked,OnChangedRender(nameof(OnSlot5Changed))]
        public ItemInfo InvSlot5{ get; set; }
        
        [Networked,OnChangedRender(nameof(OnSlot5Unlocked))]
        public NetworkBool InvSlot5Unlocked{ get; set; }
        
        [Networked,OnChangedRender(nameof(OnSlotIndexChange))]
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

        private void OnSlot1Changed()
        {
            if (HasStateAuthority)
            {
                m_EventCenter.OnSlotChange.OnNext(new PlayerItemSlotChangeEvent()
                {
                    Slot = 0,
                    ItemInfo = InvSlot1
                });
            }
        }

        private void OnSlot2Changed()
        {
            if (HasStateAuthority)
            {
                m_EventCenter.OnSlotChange.OnNext(new PlayerItemSlotChangeEvent()
                {
                    Slot = 1,
                    ItemInfo = InvSlot2
                });
            }
        }

        private void OnSlot3Changed()
        { 
            if (HasStateAuthority)
            {
                m_EventCenter.OnSlotChange.OnNext(new PlayerItemSlotChangeEvent()
                {
                    Slot = 2,
                    ItemInfo = InvSlot3
                });
            }
        }

        private void OnSlot4Changed()
        { 
            if (HasStateAuthority)
            {
                m_EventCenter.OnSlotChange.OnNext(new PlayerItemSlotChangeEvent()
                {
                    Slot = 3,
                    ItemInfo = InvSlot4
                });
            }
        }

        private void OnSlot5Changed()
        { 
            if (HasStateAuthority)
            {
                m_EventCenter.OnSlotChange.OnNext(new PlayerItemSlotChangeEvent()
                {
                    Slot = 4,
                    ItemInfo = InvSlot5
                });
            }
        }

        private void OnSlotIndexChange()
        {
            m_EventCenter.OnInventoryIndexChange.OnNext(InventorySlotIndex);
        }

        private void OnSlot5Unlocked()
        {
            m_EventCenter.OnSlot5Unlock.OnNext(InvSlot5Unlocked);
        }
    }
}