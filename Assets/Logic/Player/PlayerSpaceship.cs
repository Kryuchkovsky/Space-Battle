using System;
using Cinemachine;
using UnityEngine;

namespace Logic.Player
{
    public class PlayerSpaceship : MonoBehaviour
    {
        public event Action OnClick;
        
        [SerializeField] private CinemachineVirtualCamera _camera;

        public int CameraPriority
        {
            get => _camera.Priority;
            set => _camera.Priority = value;
        }

        private void OnMouseDown() => OnClick?.Invoke();
    }
}
