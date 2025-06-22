using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Addons.FSM;
using TaterGame.SnowIsland;
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

        private StateMachine<StateBehaviour> m_LocomotionMachine;
        private StateMachine<StateBehaviour> m_ItemMachine;
        
        public void CollectStateMachines(List<IStateMachine> stateMachines)
        {
            StateBehaviour[] locomotionStates = m_LocomotionStateRoot.GetComponents<StateBehaviour>();
            m_LocomotionMachine = new StateMachine<StateBehaviour>("Locomotion", locomotionStates);
            m_LocomotionMachine.SetDefaultState(locomotionStates.First(x => x is PlayerNormalLocomotionState).StateId);
            stateMachines.Add(m_LocomotionMachine);
            
            StateBehaviour[] itemStates = m_ItemStateRoot.GetComponents<StateBehaviour>();
            m_ItemMachine = new StateMachine<StateBehaviour>("Item", itemStates);
            m_ItemMachine.SetDefaultState(itemStates.First(x => x is PlayerIdleItemState).StateId);
            stateMachines.Add(m_ItemMachine);
        }
    }
}