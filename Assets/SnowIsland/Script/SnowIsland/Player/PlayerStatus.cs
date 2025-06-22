using Fusion;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerStatus: NetworkBehaviour, IDamagable,IDamageSource
    {        
        [ShowInInspector]
        public PlayerStatusData PlayerStatusData { get; set; } = new PlayerStatusData
        {
            Health = 1000,
            HealthMax = 1000,
            Warmth = 200,
            WarmthMax = 200,
            Food = 200,
            FoodMax = 200
        };
        string IDamagable.DisplayName { get=>$"疯狂的地蛋({this.Object.Id})"; }

        string IDamageSource.DisplayName { get=>$"疯狂的地蛋-({this.Object.Id})"; }

        [SerializeField]
        private ModelFlicker m_ModelFlicker;

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_TakeDamage(NetworkBehaviour playerAttackItemState, int damage)
        {
            if (!(playerAttackItemState is IDamageTrigger trigger))
            {
                return;
            }
            
            var data = PlayerStatusData;
            data.Health -= damage;
            PlayerStatusData = data;
            m_ModelFlicker?.Flicker();
        }
    }
}