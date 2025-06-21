using Animancer;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class PlayerAnimation: MonoBehaviour
    {
        [SerializeField]
        private ClipTransition m_Death;

        [SerializeField]
        private ClipTransition m_Revive;

        [SerializeField]
        private LinearMixerTransition m_Movement;
    }
}