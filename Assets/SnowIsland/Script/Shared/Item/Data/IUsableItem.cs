namespace TaterGame.SnowIsland.Shared
{
    public interface IUsableItem
    {
        public float UseDuration { get; }
        public int AnimIndex { get; }
    }
}