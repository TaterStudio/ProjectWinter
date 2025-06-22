using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerIdleItemState: PlayerBaseItemState
    {
        private ItemService m_ItemService;
        private PlayerUseItemState m_PlayerUseItemState;
        private PlayerAttackItemState m_PlayerAttackItemState;
        private PlayerInventoryState m_PlayerInventoryState;
        protected override void Awake()
        {
            base.Awake();
            m_PlayerUseItemState = GetStateComponent<PlayerUseItemState>();
            m_PlayerAttackItemState = GetStateComponent<PlayerAttackItemState>();
            m_ItemService = ApplicationScope.Resolve<ItemService>();
            m_PlayerInventoryState = GetComponentInParent<PlayerInventoryState>();
        }
        
        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Idle;
        }
        

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            if (!HasInput)
            {
                return;
            }
            
            bool isUse = m_InputData.NetworkButtons.IsSet(PlayerInputButton.Use);
            
            if (!isUse)
            {
                return;
            }

            var itemInfo = m_PlayerInventoryState.CurrentItemInfo;
            var item = m_ItemService.GetItem(itemInfo.ItemId);

            switch (item.ItemDataSo)
            {
                case IMeleeItem meleeItem:
                    if(PlayerState.CanAttack())
                    {
                        m_PlayerAttackItemState.MeleeItem = meleeItem;
                        Machine.TryActivateState(m_PlayerAttackItemState.StateId);
                    }
                    break;
            }
        }
    }
}