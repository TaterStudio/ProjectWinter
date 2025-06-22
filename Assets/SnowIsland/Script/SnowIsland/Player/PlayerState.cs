using System;
using Fusion;
using TaterGame.SnowIsland.Shared;

namespace TaterGame.SnowIsland
{
    public class PlayerState: NetworkBehaviour
    {
        [Networked]
        public PlayerItemState ItemState { get; set; }
        [Networked]
        public PlayerLocomotionState LocomotionState { get; set; }

        [Networked]
        public PlayerStatusData PlayerStatusData { get; set; }
        
        [Networked]
        public RadioType RadioType { get; set; }
        [Networked]
        public ItemInfo InvSlot1 { get; set; }
        [Networked]
        public ItemInfo InvSlot2 { get; set; }
        [Networked]
        public ItemInfo InvSlot3 { get; set; }
        [Networked]
        public ItemInfo InvSlot4 { get; set; }
        [Networked]
        public ItemInfo InvSlot5 { get; set; }
        [Networked]
        public bool InvSlot5Unlocked { get; set; }
        
        /// <summary>
        /// 是否正在跑步，如果是，则用2点饱食度换1点温暖度
        /// 默认情况下温暖度是每秒-2，跑步状态下是每秒-1
        /// </summary>
        [Networked]
        public bool IsRun { get; set; }

        public bool CanMove()
        {
            return LocomotionState is not (PlayerLocomotionState.Interact);
        }

        public bool CanInteract()
        {
            return (LocomotionState is PlayerLocomotionState.Normal) && (ItemState is PlayerItemState.Idle);
        }

        public bool CanRun()
        {
            if (LocomotionState is PlayerLocomotionState.Normal)
            {
                return PlayerStatusData.Food > 0;
            }

            if (LocomotionState is PlayerLocomotionState.Ghost)
            {
                return true;
            }

            return false;
        }

        public bool CanAttack()
        {
            return (LocomotionState is PlayerLocomotionState.Normal) && (ItemState is PlayerItemState.Idle);
        }
    }

    /// <summary>
    /// 玩家物品状态
    /// 当PlayerLocomotionState为Normal的时候会有这些状态
    /// </summary>
    public enum PlayerItemState
    {
        Idle,
        Aim,
        Use,
        Shoot,
    }

    public enum PlayerLocomotionState
    {
        /// <summary>
        /// 正常的状态
        /// 可以使用物品，瞄准，攻击
        /// </summary>
        Normal,
        
        /// <summary>
        /// 到底状态
        /// 只能移动，无法做任何其他操作
        /// </summary>
        Incap,
        
        /// <summary>
        /// 死亡状态 
        /// </summary>
        Ghost,
        
        /// <summary>
        /// 交互状态
        /// </summary>
        Interact,
        
        /// <summary>
        /// 持有电池状态
        /// </summary>
        Hold,
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