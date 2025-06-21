using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
    
namespace TaterGame.SnowIsland
{
    public class ApplicationScope: LifetimeScope
    {
        private static ApplicationScope s_Instance;
        
        [SerializeField]
        private PlayerInput m_PlayerInput;
        protected override void Awake()
        {
            base.Awake();
            s_Instance = this;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.Register<EventCenter>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponent(m_PlayerInput).AsSelf();
        }

        public static T Resolve<T>()
        {
            return s_Instance.Container.Resolve<T>();
        }
    }
}