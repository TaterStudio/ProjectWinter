using System;
using System.Collections.Generic;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland
{
    public class IndicatorCanvasController: MonoBehaviour
    {
        [SerializeField]
        private Camera m_IndicatorCamera;

        private UIDocument m_UIDocument;
        
        private EventCenter m_EventCenter;

        private IndicatorCenter m_Center;

        private VisualElement m_Root;

        private Dictionary<IndicatorItem, IndicatorViewItem> m_IndicatorPair = new();
        private void Awake()
        {
            m_UIDocument = GetComponent<UIDocument>();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.SetIndicatorCenter.Subscribe(OnCenterChange).AddTo(this);
            m_EventCenter.OnIndicatorItemSpawn.Subscribe(AddIndicatorItem).AddTo(this);
            m_EventCenter.OnIndicatorItemDestroyed.Subscribe(RemoveIndicatorItem).AddTo(this);
            m_Root = m_UIDocument.rootVisualElement;
        }
        private void AddIndicatorItem(IndicatorItem item)
        {
            if (!m_IndicatorPair.ContainsKey(item))
            {
                var indicatorViewItem = new IndicatorViewItem();
                indicatorViewItem.SetImage(item.IndicatorTexture);
                m_IndicatorPair.Add(item,indicatorViewItem);
                m_Root.Add(indicatorViewItem);
                // 在这里添加其他逻辑，例如显示指示器
                Debug.Log($"Added indicator item: {item}");
            }
            else
            {
                Debug.LogError("错误，添加了两次物品");
            }
        }

        private void RemoveIndicatorItem(IndicatorItem item)
        {
            if (m_IndicatorPair.ContainsKey(item))
            {
                var view = m_IndicatorPair[item];
                view.RemoveFromHierarchy();
                m_IndicatorPair.Remove(item);
                // 在这里添加其他逻辑，例如移除指示器
                Debug.Log($"Removed indicator item: {item}");
            }
            else
            {
                Debug.LogError("错误，东西本就不存在");
            }
        }


        private void OnCenterChange(IndicatorCenter center)
        {
            m_Center = center;
        }

        private void LateUpdate()
        {
            if (m_Center == null)
            {
                return;
            }

            foreach (var indicatorKVP in m_IndicatorPair)
            {
                var item = indicatorKVP.Key;
                var view = indicatorKVP.Value;
                
                var worldPos = item.IndicatorPosition;
                var localPos = RuntimePanelUtils.CameraTransformWorldToPanel(m_Root.panel, worldPos, m_IndicatorCamera);
                view.SetPosition(localPos.x,localPos.y);
            }
        }
    }
}