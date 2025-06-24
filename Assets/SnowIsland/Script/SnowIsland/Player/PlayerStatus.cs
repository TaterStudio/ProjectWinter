using System;
using Cysharp.Threading.Tasks;
using Fusion;
using Sirenix.OdinInspector;
using TaterGame.SnowIsland.Shared;
using UniRx;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerStatus: NetworkBehaviour, IDamagable,IDamageSource
    {        

        public int Health = 1000;

        public int HealthMax = 1000;

        public int Warmth = 200;

        public int WarmthMax = 200;

        public int Food = 200;

        public int FoodMax = 200;
        
        string IDamagable.DisplayName { get=>$"疯狂的地蛋({this.Object.Id})"; }

        string IDamageSource.DisplayName { get=>$"疯狂的地蛋-({this.Object.Id})"; }

        [SerializeField]
        private ModelFlicker m_ModelFlicker;

        private EventCenter m_EventCenter;
        private IngamePlayerSetting m_IngamePlayerSetting;

        public bool IsDead;
        public bool IsHeating;
        public bool IsRunning=>m_PlayerState.IsRun;

        private CompositeDisposable m_AliveListeners = new();
        private PlayerState m_PlayerState;
        
        private void Awake()
        {
            m_PlayerState = GetComponent<PlayerState>();
            m_AliveListeners.AddTo(this);
            m_IngamePlayerSetting = ApplicationScope.Resolve<IngamePlayerSetting>();
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            StartStatusTicker();
        }

        public void StartStatusTicker()
        {
            var intervaler = Observable.Interval(TimeSpan.FromSeconds(1/m_IngamePlayerSetting.TickFrequency));
            intervaler.Subscribe(StepWarmth).AddTo(m_AliveListeners);
            intervaler.Subscribe(StepFood).AddTo(m_AliveListeners);
            intervaler.Subscribe(StepHealth).AddTo(m_AliveListeners);
        }

        public void StopStatusTicker()
        {
            m_AliveListeners.Clear();
        }

        public override void Spawned()
        {
            NotifyFoodUpdate();
            NotifyWarmthUpdate();
            NotifyHealthUpdate();
        }

        private void StepHealth(long _)
        {
            int nextHealth = Health;
            if (Warmth <= 0)
            {
                nextHealth = Health - 1;
            }
            
            // ReSharper disable once PossibleLossOfFraction
            // 生命值超过90，则什么
            if (ShouldTryRecoverHealth() && Food > 0 && Warmth > 0)
            {
                nextHealth += m_IngamePlayerSetting.HealthNaturalRecover;
            }
            nextHealth = Mathf.Clamp(nextHealth, 0, HealthMax);
            Health = nextHealth;
            NotifyHealthUpdate();
        }

        private void StepWarmth(long _)
        {
            int nextWarmth = Warmth + m_IngamePlayerSetting.WarmthGainPerSecond;
            if (IsRunning)
            {
                nextWarmth += m_IngamePlayerSetting.WarmthGainRunning;
            }
            nextWarmth = Mathf.Clamp(nextWarmth, 0, WarmthMax);
            Warmth = nextWarmth;
            NotifyWarmthUpdate();
        }

        private void StepFood(long _)
        {
            int nextFood = Food + m_IngamePlayerSetting.FoodGainPerSecond;
            if (IsRunning)
            {
                nextFood += m_IngamePlayerSetting.FoodGainRunning;
            }

            if (ShouldTryRecoverHealth() && nextFood > 0)
            {
                nextFood += m_IngamePlayerSetting.HealthNaturalRecoverFoodGain;
            }
            nextFood = Mathf.Clamp(nextFood, 0, FoodMax);
            Food = nextFood;
            NotifyFoodUpdate();
        }

        private void NotifyFoodUpdate()
        {
            m_EventCenter.PlayerFoodEvent.OnNext(new PlayerStatusEvent()
            {
                Sender = gameObject,
                CurrentValue = Food,
                MaxValue = FoodMax
            });
        }

        public void ApplyFood(FoodData foodData)
        {
            int health = Mathf.Clamp(Health * (1 + (foodData.HealthPercent/100) ) + foodData.Health, 0, foodData.MaxHealth);
            int warmth = Mathf.Clamp(Warmth * (1 + (foodData.WarmthPercent/100)) + foodData.Warmth, 0, foodData.MaxWarmth);
            int food = Mathf.Clamp(Food * (1 + (foodData.FoodPercent/100)) + foodData.Food, 0, foodData.MaxFood);
            int healthMax = Mathf.Clamp(HealthMax * (1 + (foodData.MaxHealthPercent/100)) + foodData.Health, 0, 10000);
            int warmthMax = Mathf.Clamp(WarmthMax * (1 + (foodData.MaxWarmthPercent/100)) + foodData.Warmth, 0,10000);
            int foodMax = Mathf.Clamp(FoodMax * (1 + (foodData.MaxFoodPercent/100)) + foodData.Food, 0,10000);
            int maxHealthMaxDiff = healthMax - HealthMax;
            int maxWarmthMaxDiff = warmthMax - WarmthMax;
            int maxFoodMaxDiff = foodMax - FoodMax;
            Health = health + maxHealthMaxDiff;
            Warmth = warmth + maxWarmthMaxDiff;
            Food = food + maxFoodMaxDiff;
            HealthMax = healthMax;
            WarmthMax = warmthMax;
            FoodMax = foodMax;
            NotifyFoodUpdate();
            NotifyWarmthUpdate();
        }

        private bool ShouldTryRecoverHealth()
        {
            return (HealthMax-Health) * 100 / HealthMax >= m_IngamePlayerSetting.HealthNaturalRecoverThresholdIn100;
        }

        private void NotifyWarmthUpdate()
        {
            m_EventCenter.PlayerWarmthEvent.OnNext(new PlayerStatusEvent()
            {
                Sender = gameObject,
                CurrentValue = Warmth,
                MaxValue = WarmthMax
            });
        }

        private void NotifyHealthUpdate()
        {
            m_EventCenter.PlayerHealthEvent.OnNext(new PlayerStatusEvent()
            {
                Sender = gameObject,
                CurrentValue = Health,
                MaxValue = HealthMax
            });
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void RPC_TakeDamage(NetworkBehaviour playerAttackItemState, int damage)
        {
            if (!(playerAttackItemState is IDamageTrigger trigger))
            {
                return;
            }
            Health -= damage; 
            if(HasStateAuthority)
            {
                NotifyHealthUpdate();
            }
            m_ModelFlicker?.Flicker();
        }
    }
}