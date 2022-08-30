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
        [SerializeField] private GameplayInterface _gameplayInterface;
        [SerializeField] private ClueView _clueView;
        
        private Spaceship _player;
        private bool _gameIsStarted;

        private void Awake()
        {
            _battleBuilder.InputHandler = _gameplayInterface.InputHandler;
            _player = _battleBuilder.CreatePlayer();
            
            _cameraManager.Init(_player.transform, _player.Camera);
            _cameraManager.ShowPlayer();

            _clueView.SetStatus(true);
            _clueView.SetScalingStatus(true);
            _clueView.SetText("Click on the spaceship to start the game");
            _gameplayInterface.gameObject.SetActive(false);
            
            _player.TouchAgent.OnTouch += StartGame;
            _gameplayInterface.NextWeaponButton.onClick.AddListener(_player.NextWeapon);
            _gameplayInterface.PreviousWeaponButton.onClick.AddListener(_player.NextWeapon);
        }

        private void OnDestroy()
        {
            _player.TouchAgent.OnTouch -= StartGame;
            _gameplayInterface.NextWeaponButton.onClick.RemoveListener(_player.NextWeapon);
            _gameplayInterface.PreviousWeaponButton.onClick.RemoveListener(_player.NextWeapon);
        }

        public void StartGame()
        {
            if (_gameIsStarted)
            {
                return;
            }
            
            _battleBuilder.StartBattle();
            _cameraManager.ShowBattle();
            _clueView.SetStatus(false);
            _gameplayInterface.gameObject.SetActive(true);
            _gameIsStarted = true;
        }
    }
}