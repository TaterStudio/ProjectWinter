using System;
using Fusion.Addons.FSM;
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
        private PlayerCharacter m_PlayerCharacter;
        private PlayerStatus m_PlayerStatus;
        public IMeleeItem MeleeItem { get; set; }
        private Vector3 m_AtkCenter;
        private bool m_ShouldTransit = false;
        private bool m_Attacked;
        private EventCenter m_EventCenter;
        /// <summary>
        /// 是否是成功完成的交互，
        /// 如果是动画播放完成的交互，则返回true
        /// 其他情况则为false
        /// </summary>
        private bool m_IsSuccess = false;
        protected override void Awake()
        {
            base.Awake();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_PlayerCharacter = GetComponentInParent<PlayerCharacter>();
            m_PlayerAnimation = GetComponentInParent<PlayerAnimation>();
            m_PlayerStatus = GetComponentInParent<PlayerStatus>();
            m_PlayerAnimation.OnExitAtk.Subscribe(x =>
            {
                m_ShouldTransit = true;
            });
        }

        protected override PlayerItemState GetItemState()
        {
            return PlayerItemState.Attack;
        }

        protected override void OnEnterState()
        {
            m_ShouldTransit = false;
            m_PlayerAnimation.RPC_PlayAtkRpc();
            m_AnimatorMonitor.OnInvokeInvokeAttack.AddListener(InvokeAttack);
            m_Attacked = false;
            m_IsSuccess = false;
        }

        protected override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            if (m_ShouldTransit)
            {
                m_ShouldTransit = false;
                m_IsSuccess = true;
                Machine.TryActivateState<PlayerIdleItemState>();
            }
        }

        protected override void OnExitState()
        {
            m_PlayerAnimation.RPC_SetDefault();
            m_AnimatorMonitor.OnInvokeInvokeAttack.RemoveListener(InvokeAttack);
            m_EventCenter.PlayerInteractSuccess.OnNext(new PlayerEndInteractEvent()
            {
                Sender = m_PlayerCharacter.gameObject,
                Success = m_IsSuccess
            });
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