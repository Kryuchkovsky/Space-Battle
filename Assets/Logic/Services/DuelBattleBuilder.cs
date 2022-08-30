using Logic.Patterns;
using Logic.Spaceships;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Services
{
    public class DuelBattleBuilder : BaseBattleBuilder
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;

        private RandomSpaceshipFactory<Spaceship> _spaceshipFactory;
        private ObjectPool<Effect> _destructionEffectPool;
        private Spaceship _player;
        private Spaceship _enemy;
        
        private void Awake()
        {
            _spaceshipFactory = new RandomSpaceshipFactory<Spaceship>(_data.Spaceships, transform);
            _destructionEffectPool = new ObjectPool<Effect>(_data.DestructionEffect, transform);
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
            _enemy.Init(new VulnerableState(), new PursuingBehavior(), new BotShootingBehavior());
            _enemy.Target = _player.transform;
            _player.Init(new VulnerableState(), new PursuingBehavior(), new PlayerShootingBehavior(InputHandler));
            _player.Target = _enemy.transform;
            _player.OnSpaceshipDestroy += () => _destructionEffectPool.Take(_player.transform.position);
            _enemy.OnSpaceshipDestroy += () => _destructionEffectPool.Take(_enemy.transform.position);
        }
    }
}