using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerIdleItemState: PlayerBaseItemState
    {
        private PlayerInputData m_InputData;
        private ItemService m_ItemService;
        private PlayerUseItemState m_PlayerUseItemState;
        private PlayerAttackItemState m_PlayerAttackItemState;
        private PlayerInventoryState m_PlayerInventoryState;
        private void Awake()
        {
            m_PlayerUseItemState = GetComponent<PlayerUseItemState>();
            m_PlayerAttackItemState = GetComponent<PlayerAttackItemState>();
            m_ItemService = ApplicationScope.Resolve<ItemService>();
            m_PlayerInventoryState = GetComponentInParent<PlayerInventoryState>();
        }
        
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Idle;
        }

        protected override void OnFixedUpdate()
        {
            if (!GetInput<PlayerInputData>(out m_InputData))
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
                    m_PlayerAttackItemState.Duration = meleeItem.UseDuration;
                    Machine.TryActivateState(m_PlayerAttackItemState.StateId);
                    break;
            }
        }
    }
}