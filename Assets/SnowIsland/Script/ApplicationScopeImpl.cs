using TaterGame.SnowIsland.Dev;
using TaterGame.SnowIsland.Shared;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;
    
namespace TaterGame.SnowIsland
{
    public class ApplicationScopeImpl: ApplicationScope
    {
        [SerializeField]
        private CommandTableSO m_CommandTableSO;
        [SerializeField]
        private GamePlaySettingSO m_GameplaySettingSO;
        [SerializeField]
        private InteractionTableSO m_InteractTableSO;
        [SerializeField]
        private PlayerInput m_PlayerInput;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(m_CommandTableSO).AsSelf();
            builder.RegisterInstance(m_GameplaySettingSO.IngamePlayerSetting).AsSelf();
            builder.Register<EventCenter>(Lifetime.Singleton).AsSelf();
            builder.Register<ClientState>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<ItemService>().AsSelf();
            builder.RegisterComponent(m_PlayerInput).AsSelf();
            builder.RegisterEntryPoint<ItemDropperCommand>();
            builder.RegisterInstance(m_InteractTableSO).AsSelf();
            builder.RegisterEntryPoint<InteractDroppedController>();
            builder.RegisterEntryPoint<FrameRateLocker>();
        }
    }
}