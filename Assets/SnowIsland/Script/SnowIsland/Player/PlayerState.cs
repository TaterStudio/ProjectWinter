using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    //todo: 迁移到PlayerStateMachine
    [DisallowMultipleComponent]
    public class PlayerState: NetworkBehaviour
    {
        [field: SerializeField]
        public PlayerItemState ItemState { get; set; }
        [field: SerializeField]
        public PlayerLocomotionState LocomotionState { get; set; }


        /// <summary>
        /// 是否正在跑步，如果是，则用2点饱食度换1点温暖度
        /// 默认情况下温暖度是每秒-2，跑步状态下是每秒-1
        /// </summary>
        [Networked]
        public bool IsRun { get; set; }

        public Vector2 MoveDir { get; set; }

        public bool CanMove()
        {
            return LocomotionState is not (PlayerLocomotionState.Interact or PlayerLocomotionState.FakeDead);
        }

        public bool CanInteract()
        {
            return (LocomotionState is PlayerLocomotionState.Normal) && (ItemState is PlayerItemState.Idle);
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
        //使用，可以中途暂停
        Use,
        //无法暂停
        Shoot,
        //使用，无法暂停
        Attack,
        
        /// <summary>
        /// 切换中，无法进行其他操作
        /// </summary>
        Switch
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
        /// 假死状态
        /// </summary>
        FakeDead,
        
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
    
}