using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Addons.FSM;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerStateMachine: NetworkBehaviour, IStateMachineOwner
    {
        [SerializeField]
        private GameObject m_LocomotionStateRoot;

        [SerializeField]
        private GameObject m_ItemStateRoot;

        public StateMachine<StateBehaviour> ItemMachine => m_ItemMachine;
        private StateMachine<StateBehaviour> m_LocomotionMachine;
        private StateMachine<StateBehaviour> m_ItemMachine;
        private EventCenter m_EventCenter;
        private bool m_ShouldBecomeGhost = false;
        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.PlayerHealthEvent.Subscribe(OnHealthChange);
        }

        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            StateBehaviour[] locomotionStates = m_LocomotionStateRoot.GetComponentsInChildren<StateBehaviour>();
            m_LocomotionMachine = new StateMachine<StateBehaviour>("Locomotion", locomotionStates);
            m_LocomotionMachine.SetDefaultState(locomotionStates.First(x => x is PlayerNormalLocomotionState).StateId);
            stateMachines.Add(m_LocomotionMachine);
            
            StateBehaviour[] itemStates = m_ItemStateRoot.GetComponentsInChildren<StateBehaviour>();
            m_ItemMachine = new StateMachine<StateBehaviour>("Item", itemStates);
            m_ItemMachine.SetDefaultState(itemStates.First(x => x is PlayerIdleItemState).StateId);
            stateMachines.Add(m_ItemMachine);
        }

        public void TryMarkItemIdle()
        {
            m_ItemMachine.TryActivateState<PlayerIdleItemState>();
        }

        public void MarkItemIdle()
        {
            m_ItemMachine.ForceActivateState<PlayerIdleItemState>();
        }

        private void OnHealthChange(PlayerStatusEvent evt)
        {
            if (evt.Sender == null || evt.Sender != gameObject)
            {
                return;
            }
            if (evt.CurrentValue <= 0)
            {
                m_ShouldBecomeGhost = true;
            }
        }

        public override void FixedUpdateNetwork()
        {
            if (m_ShouldBecomeGhost)
            {
                m_ShouldBecomeGhost = false;
                m_LocomotionMachine.TryActivateState<PlayerGhostLocomotionState>();
            }
        }

#if UNITY_EDITOR

        [Button]
        public void GhostStateEditor()
        {
            if (!Application.isPlaying)
            {
                return;
            }
            m_ShouldBecomeGhost = true;
        }
#endif
    }
}