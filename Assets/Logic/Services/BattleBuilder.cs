using System.Collections;
using Logic.Data;
using Logic.Spaceships;
using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Services
{
    public class BattleBuilder : MonoBehaviour
    {
        [SerializeField] private LevelData _data;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemiesSpawnPoint;
        [SerializeField] private Effect _destructionEffect;
        [SerializeField] [Min(0)] private float _spawnInterval = 5;
        [SerializeField] [Min(0)] private float _range = 150; 

        private RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship> _playerSpaceshipFactory;
        private RandomSpaceshipFactory<VulnerableUnarmedMovingSpaceship> _enemiesSpaceshipFactory;
        private ObjectPool<Effect> _destructionEffectPool;
        private VulnerableUnarmedMovingSpaceship _enemy;
        private WaitForSeconds _interval;
        private bool _canSpawn = true;

        public void Init()
        {
            _playerSpaceshipFactory = new RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship>(_data.PlayerSpaceships, transform);
            _enemiesSpaceshipFactory = new RandomSpaceshipFactory<VulnerableUnarmedMovingSpaceship>(_data.EnemySpaceships, transform);
            _destructionEffectPool = new ObjectPool<Effect>(_destructionEffect, transform);
            _interval = new WaitForSeconds(_spawnInterval);
        }

        public InvulnerableArmedStandingSpaceship CreatePlayer()
        {
            var player = _playerSpaceshipFactory.Create();
            player.transform.position = _playerSpawnPoint.position;
            player.transform.rotation = Quaternion.LookRotation(_playerSpawnPoint.forward);

            return player;
        }

        public void LaunchSpawn() => StartCoroutine(StartCreatingEnemies());

        private IEnumerator StartCreatingEnemies()
        {
            while (true)
            {
                if (!_canSpawn)
                {
                    yield return null;
                }
                
                var position = _enemiesSpawnPoint.position + new Vector3(
                    Random.Range(-_range, _range),
                    Random.Range(-_range, _range), 
                    Random.Range(-_range, _range));

                var rotation = Quaternion.LookRotation(_enemiesSpawnPoint.forward);
                
                _enemy ??= _enemiesSpaceshipFactory.Create();
                _enemy.transform.position = position + Vector3.up * 10000;

                if (Physics.CheckBox(position, _enemy.Size / 2, rotation))
                {
                    yield return null;
                }
                else
                {
                    var enemy = _enemy;
                    enemy.transform.position = position;
                    enemy.transform.rotation = rotation;
                    enemy.OnSpaceshipDestroy += () =>
                    {
                        var effect = _destructionEffectPool.Take(enemy.transform.position);
                        effect.Callback += () => _destructionEffectPool.Return(effect);
                    };
                    _enemy = null;
                    _canSpawn = false;
                    yield return _interval;
                    _canSpawn = true;
                }
            }
        }
    }
}