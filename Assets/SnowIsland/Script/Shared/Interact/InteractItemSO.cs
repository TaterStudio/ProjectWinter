using UnityEngine;

namespace TaterGame.SnowIsland.Shared
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "TaterGame/SnowIsland/Interact", order = 0)]
    public class InteractItemSO: ScriptableObject
    {
        public delegate void OnInteractStart(GameObject interactor, GameObject target);
        public delegate void OnInteractProgress(GameObject interactor, GameObject target, float progress);
        public delegate void OnInteractComplete(GameObject interactor, GameObject target);
        public delegate void OnInteractCancel(GameObject interactor, GameObject target);
        public delegate (bool can,string failReason) CanInteract(GameObject interactor, GameObject target);
        
        public event CanInteract CanInteractEvent;
        public event OnInteractStart OnInteractStartEvent;
        public event OnInteractProgress OnInteractProgressEvent;
        public event OnInteractComplete OnInteractCompleteEvent;
        public event OnInteractCancel OnInteractCancelEvent;
        
        [SerializeField]
        private Texture2D m_Icon;
        [SerializeField]
        private string m_DisplayName;
        [SerializeField]
        private int m_AnimationIndex;

        [SerializeField]
        private string m_DisplayNameDisabled;

        public Texture2D Icon
        {
            get { return m_Icon; }
        }

        public string DisplayName
        {
            get { return m_DisplayName; }
        }

        public string DisplayNameDsiabled => m_DisplayNameDisabled;

        public int AnimationIndex => m_AnimationIndex;
        
        public (bool can, string failReason) InvokeCanInteract(GameObject interactor, GameObject target)
        {
            if (CanInteractEvent == null)
            {
                return (true,string.Empty);
            }
            return CanInteractEvent.Invoke(interactor, target);
        }

        public void StartInteract(GameObject interactor, GameObject target)
        {
            OnInteractStartEvent?.Invoke(interactor, target);
        }

        public void MakeProgress(GameObject interactor, GameObject target, float progress)
        {
            
        }

        public void CancelInteract(GameObject interactor, GameObject target)
        {
            OnInteractCancelEvent?.Invoke(interactor, target);
        }
        public void CompleteInteract(GameObject interactor, GameObject target)
        {
            OnInteractCompleteEvent?.Invoke(interactor, target);
        }
    }
}