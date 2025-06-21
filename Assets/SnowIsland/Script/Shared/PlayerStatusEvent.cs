namespace TaterGame.SnowIsland.Shared
{
    /// <summary>
    /// 玩家状态事件
    /// OnlyFor 生命值， 饱食度 温暖值
    /// </summary>
    public struct PlayerStatusEvent
    {
        public int CurrentValue;
        public int MaxValue;
    }
}