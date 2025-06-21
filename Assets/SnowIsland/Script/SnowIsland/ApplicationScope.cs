using TaterGame.SnowIsland.Shared;
using VContainer;
using VContainer.Unity;
    
namespace TaterGame.SnowIsland
{
    public class ApplicationScope: LifetimeScope
    {
        private static ApplicationScope s_Instance;
        protected override void Awake()
        {
            base.Awake();
            s_Instance = this;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            EventCenter eventCenter = new EventCenter();
            builder.RegisterInstance(eventCenter);
        }

        public static T Resolve<T>()
        {
            return s_Instance.Container.Resolve<T>();
        }
    }
}