using System;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace TaterGame.SnowIsland
{
    public class PlayerStatusDisplayController: MonoBehaviour
    {
        [SerializeField]
        private UIDocument m_UIDocument;

        private VisualElement m_Root;
        private ProgressBar m_HealthBar;
        private ProgressBar m_WarmthBar;
        private ProgressBar m_FoodBar;

        private EventCenter m_EventCenter;
        private void Awake()
        {
            SetupUI();
            return;
            
            void SetupUI()
            {
                m_Root = m_UIDocument.rootVisualElement;
                m_HealthBar = m_Root.Q<ProgressBar>("HealthBar");
                m_WarmthBar = m_Root.Q<ProgressBar>("WarmthBar");
                m_FoodBar = m_Root.Q<ProgressBar>("FoodBar");

                if (m_HealthBar == null)
                {
                    Debug.LogError("HealthBar not found");
                }
                if (m_WarmthBar == null)
                {
                    Debug.LogError("WarmthBar not found");
                }

                if (m_FoodBar == null)
                {
                    Debug.LogError("FoodBar not found");
                }
            }
        }

        private void Start()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.PlayerHealthEvent.Subscribe(OnHealthChange).AddTo(this);
            m_EventCenter.PlayerWarmthEvent.Subscribe(OnWarmthChange).AddTo(this);
            m_EventCenter.PlayerFoodEvent.Subscribe(OnHungerChange).AddTo(this);
        }

        private void OnHealthChange(PlayerStatusEvent e)
        {
            m_HealthBar.highValue = e.MaxValue;
            m_HealthBar.value = e.CurrentValue;
        }

        private void OnWarmthChange(PlayerStatusEvent e)
        {
            m_WarmthBar.highValue = e.MaxValue;
            m_WarmthBar.value = e.CurrentValue;
        }

        private void OnHungerChange(PlayerStatusEvent e)
        {
            m_FoodBar.highValue = e.MaxValue;
            m_FoodBar.value = e.CurrentValue;
        }
    }
}