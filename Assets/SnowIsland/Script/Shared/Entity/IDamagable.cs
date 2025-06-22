using Fusion;

namespace TaterGame.SnowIsland.Shared
{
    /// <summary>
    /// 被玩家攻击的
    /// </summary>
    public interface IDamagable
    {
        public void RPC_TakeDamage(NetworkBehaviour playerAttackItemState, int damage);
        public string DisplayName { get;}
    }
}