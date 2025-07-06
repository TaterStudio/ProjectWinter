 using System;
using Fusion;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class DroppedItem: NetworkBehaviour
    {
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private ItemSO m_BindItem;

        public ItemSO BindItem => m_BindItem;
        
        [SerializeField]
        private float m_PickupTime = 3;

        private EventCenter m_EventCenter;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            Observable.Timer(TimeSpan.FromSeconds(m_PickupTime))
                .Subscribe(_ => Destroy(m_Rigidbody)).AddTo(this);
        }

        public void Despawn()
        {
            gameObject.SetActive(false);
            RPC_Despawn();
        }
        [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
        private void RPC_Despawn()
        {
            Runner.Despawn(GetComponent<NetworkObject>());
        }
#if UNITY_EDITOR
        public void SetBindItemEditor(ItemSO createInfoItem)
        {
            m_BindItem = createInfoItem;
        }
#endif
    }
}