using Logic.Spaceships;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Services
{
    public class DuelBattleBuilder : BaseBattleBuilder
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;

        private RandomSpaceshipFactory<Spaceship> _spaceshipFactory;
        private Spaceship _player;
        private Spaceship _enemy;
        
        private void Awake()
        {
            _spaceshipFactory = new RandomSpaceshipFactory<Spaceship>(_data.Spaceships, transform);
        }

        public override Spaceship CreatePlayer()
        {
            if (!_player)
            {
                var rotation = Quaternion.LookRotation(_playerSpawnPoint.forward);
                _player = _spaceshipFactory.Create(_playerSpawnPoint.position, rotation);
            }

            return _player;
        }

        public override void StartBattle()
        {
            _enemy = _spaceshipFactory.Create(_enemySpawnPoint.position, Quaternion.LookRotation(_player.transform.forward));
            _enemy.Init(new VulnerableState(), new PursuingBehavior(_enemy), new BotShootingBehavior());
            _enemy.Target = _player;
            _player.Init(new VulnerableState(), new PursuingBehavior(_player), new PlayerShootingBehavior(InputHandler));
            _player.Target = _enemy;
        }
    }
}