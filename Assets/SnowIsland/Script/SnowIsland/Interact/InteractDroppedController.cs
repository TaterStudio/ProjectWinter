using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace TaterGame.SnowIsland
{
    public class InteractDroppedController: IStartable,IDisposable
    {
        [Inject]
        public InteractionTableSO InteractionTableSo;

        [Inject]
        public ClientState ClientState;
        
        [Inject]
        public EventCenter EventCenter;
        
        public void Start()
        {
            InteractionTableSo.DroppedItemPickUp.CanInteractEvent += CheckHasSpace;
            InteractionTableSo.DroppedItemPickUp.OnInteractCompleteEvent += PickUpItem;
            Debug.Log("成功注册掉落物品交互逻辑");
        }

        public void Dispose()
        {
            InteractionTableSo.DroppedItemPickUp.CanInteractEvent -= CheckHasSpace;
                InteractionTableSo.DroppedItemPickUp.OnInteractCompleteEvent -= PickUpItem;
        }

        private (bool can, string failReason) CheckHasSpace(GameObject interactor, GameObject target)
        {
            if (!interactor.TryGetComponent(out PlayerInventoryState playerInv))
            {
                return (false, "内部错误，无背包");
            }

            if (!target.TryGetComponent(out DroppedItem droppedItem)|| droppedItem.BindItem == null)
            {
                return (false, "内部错误，无物品");
            }
            
            var firstContainableSlot = playerInv.GetFirstContainableSlot(droppedItem.BindItem.Id);
            
            if (firstContainableSlot == -1)
            {
                return (false, "背包已满");
            }
            return (true, "good");
        }

        private void PickUpItem(GameObject interactor, GameObject target)
        {
            var checkHasSpace = CheckHasSpace(interactor, target);
            if (!checkHasSpace.can)
            {
                Debug.LogError("错误，击穿保护层");
                Debug.LogError(checkHasSpace.failReason);
                return;
            }
            
            if (!interactor.TryGetComponent(out PlayerInventoryState playerInv))
            {
                Debug.LogError("错误没有背包");
                return;
            }

            if (!target.TryGetComponent(out DroppedItem droppedItem))
            {
                Debug.LogError("错误没有物品");
                return;
            }

            var info = playerInv.InvSlot1;
            info.ItemId = droppedItem.BindItem.Id.GetHashCode();
            info.Amount += 1;
            playerInv.InvSlot1 = info;
            droppedItem.Despawn();
        }
    }
}