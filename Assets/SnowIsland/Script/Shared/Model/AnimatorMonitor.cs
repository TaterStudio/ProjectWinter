using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace TaterGame.SnowIsland.Shared
{
    public class AnimatorMonitor: MonoBehaviour
    {
        public UnityEvent OnInvokeInvokeAttack => m_OnInvokeAttack;

        [SerializeField]
        private UnityEvent m_OnInvokeAttack;

        public void InvokeAttack()
        {
            m_OnInvokeAttack.Invoke();
        }
    }
}