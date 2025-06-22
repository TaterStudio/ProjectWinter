namespace TaterGame.SnowIsland.Shared
{
    public interface IMeleeItem
    {
        public float AtkZOffset { get; }
        public float AtkSphereRadius { get; }
        public int AtkDamage { get; }
    }
}