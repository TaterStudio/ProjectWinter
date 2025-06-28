using VContainer;
using VContainer.Unity;

namespace TaterGame.SnowIsland.Shared
{
    public abstract class ApplicationScope: LifetimeScope
    {
        protected static ApplicationScope s_Instance;
        protected override void Awake()
        {
            base.Awake();
            s_Instance = this;
        }
        
        public static T Resolve<T>()
        {
            return s_Instance.Container.Resolve<T>();
        }
    }
}