using TaterGame.SnowIsland.Shared;
using UniRx;
using Unity.Cinemachine;
using UnityEngine;

namespace TaterGame.SnowIsland
{
    public class GameCamera: MonoBehaviour
    {
        [SerializeField]
        private CinemachineCamera m_CinemachineCamera;
 
        private EventCenter m_EventCenter;
        private void Start()
        {
            m_EventCenter = ApplicationScope.Resolve<EventCenter>();
            m_EventCenter.PlayerSpawnEvent.Subscribe(OnPlayerSpawn).AddTo(this);
        }
        private void OnPlayerSpawn(PlayerSpawnEvent e)
        {
            m_CinemachineCamera.LookAt = e.HeadPivot.transform;
            m_CinemachineCamera.Follow = e.HeadPivot.transform;
        }
    }
}