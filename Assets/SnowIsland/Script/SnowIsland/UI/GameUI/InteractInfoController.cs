using System;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland
{
    public class InteractInfoController: MonoBehaviour
    {
        [SerializeField]
        private UIDocument m_UIDocument;
        [SerializeField]
        private VisualTreeAsset m_InteractItem;
        [SerializeField]
        private VisualTreeAsset m_InteractItemRoot;
        private VisualElement m_Root;
        private VisualElement m_LastInteractable;
        private EventCenter m_EventCenter;
        private Interactable m_Interactable;

        private void Awake()
        {
            m_Root = m_UIDocument.rootVisualElement.Q<VisualElement>("InteractionRoot");
        }

        private void Start()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.PlayerCurrentInteractable.Subscribe(OnCurrentChange).AddTo(this);
        }

        private void OnCurrentChange(Interactable interactable)
        {
            if (m_LastInteractable != null)
            {
                m_LastInteractable.RemoveFromHierarchy();
            }

            m_Interactable = interactable;
            if (m_Interactable == null)
            {
                return;
            }
            m_LastInteractable = CreateInteractItem(m_Interactable);
            UpdateCurrentHudPos(m_Interactable);
            m_Root.Add(m_LastInteractable);
        }

        private void LateUpdate()
        {
            if (m_Interactable != null)
            {
                UpdateCurrentHudPos(m_Interactable);
            }
        }

        private void UpdateCurrentHudPos(Interactable interactable)
        {
            var worldPos = interactable.transform.position + interactable.InteractHudOffset;
            var localPos = RuntimePanelUtils.CameraTransformWorldToPanel(m_Root.panel, worldPos, Camera.main);
            m_LastInteractable.style.position = Position.Absolute;
            m_LastInteractable.style.left = localPos.x;
            m_LastInteractable.style.top = localPos.y;
        }

        public TemplateContainer CreateInteractItem(Interactable interactable)
        {
            var templateContainer = m_InteractItemRoot.CloneTree();
            var itemRoot = templateContainer.Q<VisualElement>("ItemRoot");
            foreach (var interactItemSo in interactable.Interaction)
            {
                var item = new InteractItem(m_InteractItem);
                item.SetIcon(interactItemSo.Icon);
                item.SetLabel(interactItemSo.DisplayName);
                item.AddTo(itemRoot);
            }
            return templateContainer;
        }
        
        private class InteractItem
        {
            private Label m_Label;
            private VisualElement m_Icon;
            private VisualElement m_ItemElement;

            public void AddTo(VisualElement root)
            {
                root.Add(m_ItemElement);
            }
            public InteractItem(VisualTreeAsset itemTree)
            {
                m_ItemElement = itemTree.CloneTree();
                m_Icon = m_ItemElement.Q<VisualElement>("InteractIcon");
                m_Label = m_ItemElement.Q<Label>("Label");
                if (m_Icon == null)
                {
                    Debug.LogError("找不到Icon");
                }

                if (m_Label == null)
                {
                    Debug.LogError("找不到Label");
                }
            }

            public void SetIcon(Texture2D icon)
            {
                m_Icon.style.backgroundImage = icon;
            }
            
            public void SetLabel(string test)
            {
                m_Label.text = test;
            }
        }
    }
}