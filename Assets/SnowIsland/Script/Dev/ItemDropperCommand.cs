using System;
using Fusion;
using HeathenEngineering.Events;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TaterGame.SnowIsland.Dev
{
    public class ItemDropperCommand: IStartable,IDisposable
    {
        [Inject]
        public ClientState ClientState;
        
        [Inject]
        public CommandTableSO CommandTableSo;
        
        [Inject]
        public ItemService ItemService;

        [Inject]
        public EventCenter EventCenter;

        public void Start()
        {
            CommandTableSo.DropItemCommand.AddListener(DropItem);    
            CommandTableSo.ListItemCommand.AddListener(ListItem);
        }

        public void Dispose()
        {
            CommandTableSo.DropItemCommand.RemoveListener(DropItem);    
            CommandTableSo.ListItemCommand.RemoveListener(ListItem);
        }

        private void DropItem(EventData<string> arg0)
        {
            if (!ItemService.TryGetItem(arg0.value, out var itemSO))
            {
                EventCenter.Log2Console.OnNext($"无法找到：{arg0.value}");
                return;
            }

            if (itemSO.DropPrefab == null)
            {
                EventCenter.Log2Console.OnNext($"无可掉的物品：{itemSO.DisplayName}");
                return;
            }
            
            var clientStateLocalPlayer = ClientState.LocalPlayer;
            var spawnPos = clientStateLocalPlayer.transform.position + new Vector3(0, 2, 0);
            var runner = ClientState.LocalRunner;
            runner.Spawn(itemSO.DropPrefab, spawnPos, Quaternion.identity);
            EventCenter.Log2Console.OnNext($"已掉落物品：{itemSO.DisplayName}在{spawnPos}");
        }

        private void ListItem(EventData arg0)
        {
            EventCenter.Log2Console.OnNext("物品列表：");
            foreach (var itemServiceItemKey in ItemService.ItemKeys)
            {
                EventCenter.Log2Console.OnNext($"- {itemServiceItemKey} : {ItemService.GetItem(itemServiceItemKey).DisplayName}");
            }
        }
    }
}