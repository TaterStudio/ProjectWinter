using Fusion.Addons.FSM;
using TaterGame.SnowIsland.Shared;

namespace TaterGame.SnowIsland
{
    public abstract class PlayerBaseItemState: StateBehaviour
    {
        protected PlayerInputData m_InputData;
        protected bool HasInput { get; private set; }
        protected PlayerState PlayerState { get; private set; }

        protected virtual void Awake()
        {
            PlayerState = GetComponentInParent<PlayerState>();
        }

        protected override void OnEnterState()
        {
            PlayerState ??= GetComponentInParent<PlayerState>();
            PlayerState.ItemState = GetItemState();
        }
        protected abstract PlayerItemState GetItemState();
        
        protected override void OnFixedUpdate()
        {
            if (PlayerState == null)
            {
                return;
            }
            
            HasInput = GetInput(out m_InputData);
        }

        protected T GetStateComponent<T>() where T : PlayerBaseItemState
        {
            return transform.parent.GetComponentInChildren<T>();
        }
    }
}