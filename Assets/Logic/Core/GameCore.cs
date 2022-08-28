using System;
using Cinemachine;
using Logic.Services;
using Logic.Spaceships;
using UnityEngine;

namespace Logic.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _initalCamera;
        [SerializeField] private BattleBuilder _battleBuilder;
        
        private InvulnerableArmedStandingSpaceship _player;
        private bool _gameIsStarted;

        private void Awake()
        {
            _battleBuilder.Init();
            _player = _battleBuilder.CreatePlayer();
            _initalCamera.Priority = _player.CameraPriority + 1;
            _player.OnClick += StartGame;
        }

        private void OnDestroy()
        {
            _player.OnClick -= StartGame;
        }

        public void StartGame()
        {
            if (_gameIsStarted)
            {
                return;
            }

            _player.Init();
            _battleBuilder.LaunchSpawn();
            _player.CameraPriority = _initalCamera.Priority + 1;
            _gameIsStarted = true;
        }
    }
}
