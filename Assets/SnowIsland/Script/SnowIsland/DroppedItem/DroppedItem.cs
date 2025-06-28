using System;
using Fusion;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class DroppedItem: NetworkBehaviour
    {
        private Rigidbody m_Rigidbody;
        
        [SerializeField]
        private float m_PickupTime = 3;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Observable.Timer(TimeSpan.FromSeconds(m_PickupTime))
                .Subscribe(_ => Destroy(m_Rigidbody)).AddTo(this);
        }
    }
}