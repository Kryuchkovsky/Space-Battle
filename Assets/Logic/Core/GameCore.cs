using Logic.Services;
using Logic.Spaceships;
using Logic.UI;
using UnityEngine;

namespace Logic.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] private BaseBattleBuilder _battleBuilder;
        [SerializeField] private CameraManager _cameraManager;
        [SerializeField] private ClueView _clueView;
        
        private InvulnerableArmedStandingSpaceship _player;
        private bool _gameIsStarted;

        private void Awake()
        {
            _battleBuilder.Init();
            _player = _battleBuilder.CreatePlayer();
            
            _cameraManager.Init(_player.transform, _player.Camera);
            _cameraManager.ShowPlayer();

            _clueView.SetStatus(true);
            _clueView.SetScalingStatus(true);
            _clueView.SetText("Click on the spaceship to start the game");
            
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
            _battleBuilder.SetSpawnStatus(true);
            _cameraManager.ShowBattle();
            _clueView.SetStatus(false);
            _gameIsStarted = true;
        }
    }
}