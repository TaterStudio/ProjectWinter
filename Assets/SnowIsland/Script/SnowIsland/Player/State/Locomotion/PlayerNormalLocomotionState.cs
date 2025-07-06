using System;
using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerNormalLocomotionState: PlayerBaseLocomotionState
    {
        private PlayerStateMachine m_MachineRoot;
        private PlayerAnimation m_PlayerAnimation;
        private PlayerStatus m_PlayerStatus;
        private Interactable m_CurrentInteractable;
        private int m_InteractIndex;
        private EventCenter m_EventCenter;
        [SerializeField]
        private PlayerInteractLocomotionState m_InteractionState;

        protected override void Awake()
        {
            base.Awake();
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_PlayerStatus = GetComponentInParent<PlayerStatus>();
            m_MachineRoot = GetComponentInParent<PlayerStateMachine>();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.OnPlayerEnterInteract.Subscribe(OnPlayerEnterInteract).AddTo(this);
            m_EventCenter.OnPlayerExitInteract.Subscribe(OnPlayerExitInteract).AddTo(this);
            m_EventCenter.PlayerInteractSuccess.Subscribe(OnPlayerInteractSuccess).AddTo(this);
        }
        
        protected override PlayerLocomotionState GetLocomotionState()
        {
            return PlayerLocomotionState.Normal;
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            bool fakeDeath = m_InputData.NetworkButtons.IsSet(PlayerInputButton.FakeDead);

            if (fakeDeath)
            {
                Machine.TryActivateState<PlayerFakeDeadLocomotionState>();
                return;
            }

            if (m_CurrentInteractable != null)
            {
                bool interact = m_InputData.NetworkButtons.IsSet(PlayerInputButton.Interact);
                if (interact)
                {
                    m_InteractionState.SetInteraction(m_CurrentInteractable.Interaction[m_InteractIndex]);
                    Machine.TryActivateState<PlayerInteractLocomotionState>();
                }
            }
            
            bool isRun = m_InputData.NetworkButtons.IsSet(PlayerInputButton.Run);
            bool canRun = m_PlayerStatus.Food > 0;
            if (isRun && canRun)
            {
                PlayerState.IsRun = true;
            }
            else
            {
                PlayerState.IsRun = false;
            }
            
        }

        private void OnPlayerInteractSuccess(PlayerEndInteractEvent endInteractEvent)
        {
            if (endInteractEvent.Sender != m_PlayerStatus.gameObject)
            {
                return;
            }

            if (!endInteractEvent.Success)
            {
                m_CurrentInteractable.OnInteractFail(m_PlayerStatus.gameObject,m_InteractIndex);
                return;
            }

            if (m_CurrentInteractable == null)
            {
                Debug.LogWarning("很奇怪，交互中的东西没有了？");
                return;
            }
            m_CurrentInteractable.OnInteractSuccess(m_PlayerStatus.gameObject,m_InteractIndex);
        }
        
        private void OnPlayerEnterInteract(Interactable interactable)
        {
            m_CurrentInteractable = interactable;
            m_EventCenter.PlayerCurrentInteractable.OnNext(m_CurrentInteractable);
            m_InteractIndex = 0;
        }

        private void OnPlayerExitInteract(Interactable interactable)
        {
            if (m_CurrentInteractable == interactable)
            {
                m_CurrentInteractable = null;
                m_EventCenter.PlayerCurrentInteractable.OnNext(m_CurrentInteractable);
                m_InteractIndex = 0;
            }
        }
        protected override void OnEnterState()
        {
            base.OnEnterState();
            m_PlayerAnimation.RPC_PlayMove();
        }

        protected override void OnExitState()
        {
            m_MachineRoot.MarkItemIdle();
        }
    }
}