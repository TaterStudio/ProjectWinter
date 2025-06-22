using TaterGame.SnowIsland.Shared;

namespace TaterGame.SnowIsland
{
    public class PlayerIdleItemState: PlayerBaseItemState
    {
        private PlayerInputData m_InputData;
        protected override PlayerItemState GetLocomotionState()
        {
            return PlayerItemState.Idle;
        }

        protected override void OnFixedUpdate()
        {
            if (!GetInput<PlayerInputData>(out m_InputData))
            {
                return;
            }
            m_InputData.NetworkButtons.IsSet(PlayerInputButton.Use);
        }
    }
}