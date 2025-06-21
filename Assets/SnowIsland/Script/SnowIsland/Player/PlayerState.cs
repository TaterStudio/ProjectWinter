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
        public int Health { get; set; }
        [Networked]
        public int HealthMax { get; set; }
        [Networked]
        public int Warmth { get; set; }
        [Networked]
        public int WarmthMax { get; set; }
        [Networked]
        public int Food { get; set; }
        [Networked]
        public int FoodMax { get; set; }
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
    }

    /// <summary>
    /// 玩家物品状态
    /// 当PlayerLocomotionState为Normal的时候会有这些状态
    /// </summary>
    public enum PlayerItemState
    {
        Idle,
        Run,
        Aim,
        Use,
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