using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerIdleItemState: PlayerBaseItemState
    {
        protected PlayerInputData m_LastInputData;
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
            m_LastInputData = m_InputData;
            base.OnFixedUpdate();

            if (!HasInput)
            {
                return;
            }

            if (UpdateDrop())
            {
                return;
            }

            if (UpdateSwitch())
            {
                return;
            }

            if (UpdateUse())
            {
                return;
            }
            return;

            bool UpdateDrop()
            {
                var drop = m_InputData.NetworkButtons.WasPressed(m_LastInputData.NetworkButtons,PlayerInputButton.Drop);
                if (!drop)
                {
                    return false;
                }
                m_PlayerInventoryState.DropCurrentItem();
                return true;
            }

            bool UpdateSwitch()
            {
                for (int i = 1; i <= 5; i++)
                {
                    var button = (PlayerInputButton) (PlayerInputButton.InvSlot1 + (i - 1));
                    if (m_InputData.NetworkButtons.WasPressed(m_LastInputData.NetworkButtons,button))
                    {
                        m_PlayerInventoryState.SetSlotIndex(i - 1);
                        return true;
                    }
                }

                return false;
            }
            
            bool UpdateUse()
            {
                bool isUse = m_InputData.NetworkButtons.IsSet(PlayerInputButton.Use);

                if (!isUse)
                {
                    return false;
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

                return true;
            }
        }
    }
}