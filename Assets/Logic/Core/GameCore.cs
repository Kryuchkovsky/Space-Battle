using System;
using Cinemachine;
using Logic.Player;
using UnityEngine;

namespace Logic.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceship _playerSpaceship;
        [SerializeField] private CinemachineVirtualCamera _initalCamera;

        private bool _gameIsStarted;

        private void Awake()
        {
            _initalCamera.Priority = _playerSpaceship.CameraPriority + 1;
            
            _playerSpaceship.OnClick += StartGame;
        }

        private void OnDestroy()
        {
            _playerSpaceship.OnClick -= StartGame;
        }

        public void StartGame()
        {
            if (_gameIsStarted)
            {
                return;
            }

            _playerSpaceship.CameraPriority = _initalCamera.Priority + 1;
            _gameIsStarted = true;
        }
    }
}
