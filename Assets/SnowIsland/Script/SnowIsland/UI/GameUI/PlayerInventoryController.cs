using System;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland
{
    public class PlayerInventoryController: MonoBehaviour
    {
        [SerializeField]
        private UIDocument m_UIDocument;

        private ItemService m_ItemService;
        private EventCenter m_EventCenter;
        private VisualElement m_Root;

        private InvSlot m_Slot1;
        private InvSlot m_Slot2;
        private InvSlot m_Slot3;
        private InvSlot m_Slot4;
        private InvSlot m_Slot5;
        private InvSlot[] m_Slots;
        private void Awake()
        {
            m_ItemService = ApplicationScope.Resolve<ItemService>();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_Root = m_UIDocument.rootVisualElement;
            m_Slot1 = new InvSlot(m_Root.Q<VisualElement>("Slot1"));
            m_Slot2 = new InvSlot(m_Root.Q<VisualElement>("Slot2"));
            m_Slot3 = new InvSlot(m_Root.Q<VisualElement>("Slot3"));
            m_Slot4 = new InvSlot(m_Root.Q<VisualElement>("Slot4"));
            m_Slot5 = new InvSlot(m_Root.Q<VisualElement>("Slot5"));
            m_Slots = new[]{m_Slot1, m_Slot2, m_Slot3, m_Slot4, m_Slot5};
            m_EventCenter.OnInventoryIndexChange.Subscribe(SetSelectedItem).AddTo(this);
            m_EventCenter.OnSlotChange.Subscribe(OnSlotChange).AddTo(this);
            m_EventCenter.OnSlot5Unlock.Subscribe(TrySlot5Unlock).AddTo(this);
            m_EventCenter.PlayerSpawnEvent.Subscribe(RefreshInv).AddTo(this);
        }

        private void RefreshInv(PlayerSpawnEvent evt)
        {
            if (!evt.PlayerObject.TryGetComponent(out PlayerInventoryState inventory))
            {
                Debug.LogError("错误，无法获取玩家背包");
            }
            SetItem(0, inventory.InvSlot1);
            SetItem(1, inventory.InvSlot2);
            SetItem(2, inventory.InvSlot3);
            SetItem(3, inventory.InvSlot4);
            SetItem(4, inventory.InvSlot5);
            
            for (var i = 0; i < m_Slots.Length-1; i++)
            {
                m_Slots[i].Unlock();
            }
        }

        private void TrySlot5Unlock(bool unlocked)
        {
            if (unlocked)
            {
                m_Slot5.Unlock();
            }
        }
        private void OnSlotChange(PlayerItemSlotChangeEvent evt)
        {
            SetItem(evt.Slot, evt.ItemInfo);
        }

        private void SetSelectedItem(int index)
        {
            for (var i = 0; i < m_Slots.Length; i++)
            {
                if (i == index)
                {
                    m_Slots[i].MarkSelected();
                }
                else
                {
                    m_Slots[i].UnmarkSelected();
                }
            }
        }
        
        public void SetItem(int index, ItemInfo itemInfo)
        {
            var invSlot = m_Slots[index];
            var itemSo = m_ItemService.GetItem(itemInfo.ItemId);
            invSlot.SetIcon(itemSo.Icon);
            invSlot.SetAmount(itemInfo.Amount);
            invSlot.SetPlaceholder(itemSo.DisplayName);
        }


        private class InvSlot
        {
            private Label m_Placeholder;
            private Label m_Amount;
            private VisualElement m_Icon;
            private VisualElement m_Root;
            private VisualElement m_Locked;
            public InvSlot(VisualElement root)
            {
                m_Root = root;
                m_Icon = root.Q<VisualElement>("ItemIcon");
                m_Amount = root.Q<Label>("Amount");
                m_Locked = root.Q<VisualElement>("Locked");
                m_Placeholder = root.Q<Label>("Placeholder");
            }

            public void SetIcon(Texture2D texture2D)
            {
                m_Icon.style.backgroundImage = texture2D;
            }

            public void SetAmount(int amount)
            {
                m_Amount.text = amount.ToString();
            }

            public void Unlock()
            {
                m_Locked.style.visibility = Visibility.Hidden;
            }

            public void MarkSelected()
            {
                m_Root.AddToClassList("item-selected");
            }

            public void UnmarkSelected()
            {
                m_Root.RemoveFromClassList("item-selected");
            }

            public void SetPlaceholder(string itemName)
            {
                m_Placeholder.text = itemName;
            }
        }
    }
}