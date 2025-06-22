using System;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    [DisallowMultipleComponent]
    public class PlayerAttackItemState: PlayerBaseItemState,IDamageTrigger
    {
        [SerializeField]
        private LayerMask m_AtkLayer;
        [SerializeField]
        private AnimatorMonitor m_AnimatorMonitor;
        private PlayerAnimation m_PlayerAnimation;
        private PlayerIdleItemState m_PlayerIdleItemState;
        private PlayerCharacter m_PlayerCharacter;
        private PlayerStatus m_PlayerStatus;
        public IMeleeItem MeleeItem { get; set; }
        private float m_Duration;
        private Vector3 m_AtkCenter;
        private bool m_Attacked;
        protected override void Awake()
        {
            base.Awake();
            m_PlayerCharacter = GetComponentInParent<PlayerCharacter>();
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_PlayerIdleItemState = GetStateComponent<PlayerIdleItemState>();
            m_PlayerStatus = GetComponentInParent<PlayerStatus>();
        }

        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Attack;
        }

        protected override void OnEnterState()
        {
            m_PlayerAnimation.RPC_PlayAtkRpc();
            m_Duration = m_PlayerAnimation.CurrentAnimationDuration(0);
            m_AnimatorMonitor.OnInvokeInvokeAttack.AddListener(InvokeAttack);
            m_Attacked = false;
        }

        protected override void OnExitState()
        {
            m_PlayerAnimation.RPC_ExitAtkAnimRpc();
            m_AnimatorMonitor.OnInvokeInvokeAttack.AddListener(InvokeAttack);
        }

        private void InvokeAttack()
        {
            if (m_Attacked)
            {
                return;
            }
            m_Attacked = true;
            var attackPivot = m_PlayerCharacter.AttackPivot.transform;
            m_AtkCenter = attackPivot.position;
            m_AtkCenter += m_PlayerCharacter.Model.forward * MeleeItem.AtkZOffset;
            Collider[] results = new Collider[8];
            var size = Physics.OverlapBoxNonAlloc(m_AtkCenter, Vector3.one * MeleeItem.AtkSphereRadius, results, Quaternion.identity,m_AtkLayer);
            for (int i = 0; i < size; i++)
            {
                var collectedCollider = results[i];
                if (collectedCollider.gameObject == PlayerState.gameObject)
                {
                    continue;
                }
                var damageable = collectedCollider.GetComponent<IDamagable>();
                if (damageable != null)
                {
                    damageable.RPC_TakeDamage(this,MeleeItem.AtkDamage);
                    Debug.Log(this.DamageSource.DisplayName +"攻击了"+damageable.DisplayName+$"伤害{MeleeItem.AtkDamage}");
                }
            }
        }

        protected override void OnFixedUpdate()
        {
            if (Machine.StateTime > m_Duration)
            {
                Machine.TryActivateState(m_PlayerIdleItemState.StateId);
            }
        }

        public IDamageSource DamageSource { get=>m_PlayerStatus; }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(MeleeItem!=null)
            {
                Gizmos.DrawWireSphere(m_AtkCenter, MeleeItem.AtkSphereRadius);
            }
        }
#endif
    }
}