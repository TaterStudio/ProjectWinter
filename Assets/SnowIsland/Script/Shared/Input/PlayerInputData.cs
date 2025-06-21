using Fusion;
using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    public struct PlayerInputData: INetworkInput
    {
        public NetworkButtons NetworkButtons;
        public Vector2 MoveDirection;
        public Vector3 AimPosition;
    }
}