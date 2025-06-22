namespace TaterGame.SnowIsland.Shared
{
    /// <summary>
    /// 玩家的武器
    /// </summary>
    public interface IDamageTrigger
    {
        public IDamageSource DamageSource { get; }
    }
}