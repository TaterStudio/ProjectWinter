using System;
using Fusion;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    /// <summary>
    /// 负责自定义玩家的模型而不是动画
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerAvatar: NetworkBehaviour
    {
        [SerializeField]
        private CharacterParts m_CharacterParts;

        [ShowInInspector]
        private bool m_RagdollEnable = false;

        private Animator m_Animator;

        private Rigidbody[] m_Rigidbodies;

        private PlayerCharacter m_Character;

        private EventCenter m_EventCenter;

        private Transform m_Model;

        private PlayerInventoryState m_PlayerInventoryState;

        private ItemService m_ItemService;

        public Transform ItemRoot => m_CharacterParts.rightWeapon;


        private void Awake()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_ItemService = ApplicationScope.Resolve<ItemService>();
            m_Character = GetComponent<PlayerCharacter>();
            m_Model = m_Character.Model;
            m_Animator = m_Model.GetComponentInChildren<Animator>();
            m_Rigidbodies = m_Model.GetComponentsInChildren<Rigidbody>();
            m_EventCenter.OnInventoryIndexChange.Subscribe(OnInvIndex).AddTo(this);
            m_EventCenter.OnSlotChange.Subscribe(OnInvSlot).AddTo(this);
            m_PlayerInventoryState = GetComponent<PlayerInventoryState>();
        }

        private void OnInvSlot(PlayerItemSlotChangeEvent _)
        {
            RPC_UpdateHandWeapon();
        }

        private void OnInvIndex(int _)
        {
            RPC_UpdateHandWeapon();
        }

        [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
        private void RPC_UpdateHandWeapon()
        {
            UpdateHandWeapon();
        }

        private void UpdateHandWeapon()
        {
            var itemId = m_PlayerInventoryState.CurrentItemInfo.ItemId;
            if (!m_ItemService.TryGetItem(itemId, out var itemSO))
            {
                m_CharacterParts.RemoveRightWeapon();
                return;
            }
            m_CharacterParts.RemoveRightWeapon();
            m_CharacterParts.ChangeRightWeapon(itemSO.ModelPrefab);
        }

        public void SetModelForward(Vector3 forward)
        {
            m_Model.forward = forward;
        }
        
//         public void ToggleRagdoll()
//         {
//             if (m_RagdollEnable)
//             {
//                 foreach (var rigidbody in m_Rigidbodies)
//                 {
//                     rigidbody.isKinematic = true;
//                 }
//                 m_Animator.enabled = true;
//                 m_RagdollEnable = false;
//             }
//             else
//             {
//                 foreach (var rigidbody in m_Rigidbodies)
//                 {
//                     rigidbody.isKinematic = false;
//                     rigidbody.linearVelocity = Vector3.zero;
//                 }
//                 m_Animator.enabled = false;
//                 m_RagdollEnable = true;
//             }
//         }
//
// #if UNITY_EDITOR
//         [Button]
//         public void ToggleRagdollEditor()
//         {
//             if(Application.isPlaying)
//             {
//                 ToggleRagdoll();
//             }
//         }
// #endif
    }
}