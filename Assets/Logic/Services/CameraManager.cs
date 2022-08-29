using Cinemachine;
using UnityEngine;

namespace Logic.Services
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _initalCamera;
        
        private CinemachineVirtualCamera _battleCamera;
        private Transform _player;

        public void Init(Transform player, CinemachineVirtualCamera battleCamera)
        {
            _player = player;
            _battleCamera = battleCamera;
            _initalCamera.Follow = player;
            _initalCamera.LookAt = player;
        }

        public void ShowBattle() => _battleCamera.Priority = _initalCamera.Priority + 1;
        public void ShowPlayer() => _initalCamera.Priority = _battleCamera.Priority + 1;
    }
}