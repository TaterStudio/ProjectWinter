using System;
using HeathenEngineering.Events;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland.Dev
{
    [DisallowMultipleComponent]
    public class PlayerCommand: MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimationEvent m_ChangeAnimationCommand;
        [SerializeField]
        private GameEvent m_ToggleRagdollCommand;

        [SerializeField]
        private IntGameEvent m_TestItemSet;

        private PlayerInventoryState m_PlayerInventory;
        private PlayerAvatar m_PlayerAvatar;
        private ItemService m_ItemService;

        private EventCenter m_EventCenter;
        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_ItemService = ApplicationScope.Resolve<ItemService>();
            m_PlayerAvatar = GetComponent<PlayerAvatar>();
            m_ToggleRagdollCommand.AddListener(ToggleRagdoll);
            m_ChangeAnimationCommand.AddListener(ChangeAnimation);
            m_TestItemSet.AddListener(SetItems);
            m_PlayerInventory = GetComponent<PlayerInventoryState>();
        }

        private void OnDestroy()
        {
            m_ToggleRagdollCommand.RemoveListener(ToggleRagdoll);
            m_ChangeAnimationCommand.RemoveListener(ChangeAnimation);
            m_TestItemSet.RemoveListener(SetItems);
        }

        private void ChangeAnimation(EventData<PlayerAnimationEventData> arg0)
        {
            m_EventCenter.Log2Console.OnNext("对不起，动画切换未完成");
        }

        private void ToggleRagdoll(EventData arg0)
        {
            m_PlayerAvatar.ToggleRagdoll();
            m_EventCenter.Log2Console.OnNext("已切换Ragdoll模式！");
        }

        private void SetItems(EventData<int> arg0)
        {
            int startIndex = arg0.value * 4;
            int endIndex = (arg0.value + 1) * 4;
            int currentIndex = 0;
            foreach (var itemServiceItemKey in m_ItemService.ItemKeys)
            {
                if (currentIndex >= startIndex && currentIndex < endIndex)
                {
                    int invSlot = currentIndex % 4;
                    m_PlayerInventory.SetItem(invSlot,itemServiceItemKey.GetHashCode());
                    m_EventCenter.Log2Console.OnNext($"找不到index为{currentIndex}的物品: {itemServiceItemKey}");
                }
                currentIndex++;
            }
        }
    }
}