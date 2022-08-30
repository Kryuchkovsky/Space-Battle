using System.Collections;
using Logic.Data;
using Logic.Patterns;
using Logic.Spaceships;
using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Services
{
    public class ShootingRangeBattleBuilder : BaseBattleBuilder
    {
        [SerializeField] private LevelData _data;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;

        private RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship> _playerSpaceshipFactory;
        private RandomSpaceshipFactory<VulnerableUnarmedMovingSpaceship> _enemiesSpaceshipFactory;
        private ObjectPool<Effect> _destructionEffectPool;
        private InvulnerableArmedStandingSpaceship _player;
        private VulnerableUnarmedMovingSpaceship _enemy;
        private WaitForSeconds _interval;
        private bool _isSpawning;

        public override void Init()
        {
            _playerSpaceshipFactory = new RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship>(_data.PlayerSpaceships, transform);
            _enemiesSpaceshipFactory = new RandomSpaceshipFactory<VulnerableUnarmedMovingSpaceship>(_data.EnemySpaceships, transform);
            _destructionEffectPool = new ObjectPool<Effect>(_data.DestructionEffect, transform);
            _interval = new WaitForSeconds(_data.SpawnInterval);
        }

        public override InvulnerableArmedStandingSpaceship CreatePlayer()
        {
            if (!_player)
            {
                var rotation = Quaternion.LookRotation(_playerSpawnPoint.forward);
                _player = _playerSpaceshipFactory.Create(_playerSpawnPoint.position, rotation);
            }

            return _player;
        }

        public override void SetSpawnStatus(bool status)
        {
            _isSpawning = status;

            if (_isSpawning)
            {
                StartCoroutine(StartCreatingEnemies());
            }
        }

        private IEnumerator StartCreatingEnemies()
        {
            while (_isSpawning)
            {
                var position = _enemySpawnPoint.position + new Vector3(
                    Random.Range(-_data.MaxSpawnRange, _data.MaxSpawnRange),
                    Random.Range(-_data.MaxSpawnRange, _data.MaxSpawnRange), 
                    Random.Range(-_data.MaxSpawnRange, _data.MaxSpawnRange));
                var rotation = Quaternion.LookRotation(-_player.transform.right);
                _enemy ??= _enemiesSpaceshipFactory.Create(position, rotation);
                var hasCollision = Physics.CheckBox(position, _enemy.Size / 2, rotation);
                _enemy.gameObject.SetActive(!hasCollision);

                if (hasCollision)
                {
                    yield return null;
                }
                else
                {
                    var enemy = _enemy;
                    enemy.transform.position = position;
                    enemy.Speed = _data.SpaceshipSpeed;
                    enemy.SetDestructionDistance(_data.DestructionDistance);
                    enemy.OnSpaceshipDestroy += () =>
                    {
                        var effect = _destructionEffectPool.Take(enemy.transform.position);
                        effect.Callback += () => _destructionEffectPool.Return(effect);
                    };
                    _enemy = null;
                    
                    yield return _interval;
                }
            }
        }
    }
}