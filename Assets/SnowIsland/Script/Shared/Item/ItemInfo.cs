using System;
using Fusion;

namespace TaterGame.SnowIsland.Shared
{
    [Serializable]
    public struct ItemInfo: INetworkStruct
    {
        public int ItemId;
        public int Amount;
        public int Metadata;
        public int Skin;
    }
}