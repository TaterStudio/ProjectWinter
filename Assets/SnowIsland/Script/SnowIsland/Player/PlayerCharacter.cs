using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerCharacter: NetworkBehaviour
    {
        public GameObject HeadPivot => m_HeadPivot;
        [SerializeField]
        private GameObject m_HeadPivot;

        public GameObject AttackPivot => m_AttackPivot;
        [SerializeField]
        private GameObject m_AttackPivot;

        public Transform Model=>m_Model;
        [SerializeField]
        private Transform m_Model;

        [SerializeField]
        private Transform m_CorpseModel;
        
        public Action OnSpawned;
        private IngamePlayerSetting m_IngamePlayerSetting;

        private void Awake()
        {
            m_IngamePlayerSetting = ApplicationScope.Resolve<IngamePlayerSetting>();
        }

        public override void Spawned()
        {
            OnSpawned?.Invoke();
        }

        [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
        public void RPC_GhostMode()
        {
            var obj = Instantiate(m_CorpseModel,Model.transform.position,Model.transform.rotation);
            obj.gameObject.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer(Constraints.Layer.PlayerGhostLayer);
            foreach (var meshRenderer in Model.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.material = m_IngamePlayerSetting.GhostMaterial;
            }
        }
    }
}