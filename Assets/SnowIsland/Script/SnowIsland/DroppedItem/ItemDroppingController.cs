using System;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TaterGame.SnowIsland
{
    public class ItemDroppingController: IInitializable,IDisposable
    {
        [Inject]
        public ItemService ItemService;

        [Inject]
        public EventCenter EventCenter;

        [Inject]
        public ClientState ClientState;
        
        private IDisposable m_ItemDropEvt;

        public void Initialize()
        {
            m_ItemDropEvt = EventCenter.ItemDropEvent.Subscribe(OnDropItem);
        }

        public void Dispose()
        {
            m_ItemDropEvt.Dispose();
        }

        private void OnDropItem(ItemDropEvent dropEvent)
        {
            var itemId = dropEvent.ItemId;
            if (!ItemService.TryGetItem(itemId, out var item))
            {
                Debug.LogError($"错误！物品服务没有物品{itemId}");
                return;
            }

            ClientState.LocalRunner.Spawn(item.DropPrefab,dropEvent.Position,dropEvent.Rotation);
            Debug.Log($"已生成物品{item.DisplayName}在{dropEvent.Position},rot{dropEvent.Rotation}");
        }
    }
}