using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TaterGame.SnowIsland
{
    public class PlayerInputPoller: SimulationBehaviour, INetworkRunnerCallbacks
    {
        private PlayerInput m_PlayerInput;

        private void Awake()
        { 
            m_PlayerInput = ApplicationScope.Resolve<PlayerInput>();
        }

        private void OnEnable()
        {
            if (Runner != null)
            {
                Runner.AddCallbacks(this);
            }
        }

        private void OnDisable()
        {
            if (Runner != null)
            {
                Runner.RemoveCallbacks(this);
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            PlayerInputData playerInputData = new PlayerInputData();

            //按钮必须按照0123的顺序排序            
            bool isRun = m_PlayerInput.actions[Constraints.Input.Sprint].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.Run, isRun);
            
            bool isFakeDead = m_PlayerInput.actions[Constraints.Input.FakeDead].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.FakeDead, isFakeDead);

            bool isUse = m_PlayerInput.actions[Constraints.Input.Attack].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.Use, isUse);
            
            var moveDir = m_PlayerInput.actions[Constraints.Input.Move].ReadValue<Vector2>();
            playerInputData.MoveDirection = moveDir;

            var interact = m_PlayerInput.actions[Constraints.Input.Interact].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.Interact, interact);
            
            var radio = m_PlayerInput.actions[Constraints.Input.Radio].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.Radio,radio);

            var inv1 = m_PlayerInput.actions[Constraints.Input.InvSlot1].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.InvSlot1,inv1);

            var inv2 = m_PlayerInput.actions[Constraints.Input.InvSlot2].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.InvSlot2, inv2);

            var inv3 = m_PlayerInput.actions[Constraints.Input.InvSlot3].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.InvSlot3, inv3);

            var inv4 = m_PlayerInput.actions[Constraints.Input.InvSlot4].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.InvSlot4, inv4);

            var inv5 = m_PlayerInput.actions[Constraints.Input.InvSlot5].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.InvSlot5, inv5);
            
            var drop = m_PlayerInput.actions[Constraints.Input.Drop].IsPressed();
            playerInputData.NetworkButtons.Set(PlayerInputButton.Drop, drop);
            
            input.Set(playerInputData);
            
            
        }

        #region 未使用的

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }

        #endregion
    }
}