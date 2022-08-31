using System.Collections;
using Logic.Spaceships;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Services
{
    public class ShootingRangeBattleBuilder : BaseBattleBuilder
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private DistanceDestructor _distanceDestructor;
        [SerializeField] [Min(0)] private float _maxSpawnRange = 250;
        [SerializeField] [Min(0)] private float _spawnInterval = 5;

        private RandomSpaceshipFactory<Spaceship> _spaceshipFactory;
        private RandomSpaceshipFactory<Spaceship> _unfinishedSpaceshipFactory;
        private Spaceship _player;
        private Spaceship _enemy;
        private WaitForSeconds _interval;
        private bool _isSpawning;

        private void Awake()
        {
            _spaceshipFactory = new RandomSpaceshipFactory<Spaceship>(_data.Spaceships, transform);
            _unfinishedSpaceshipFactory = new RandomSpaceshipFactory<Spaceship>(_data.UnfinishedSpaceships, transform);
            _interval = new WaitForSeconds(_spawnInterval);
        }

        public override Spaceship CreatePlayer()
        {
            if (!_player)
            {
                var rotation = Quaternion.LookRotation(_playerSpawnPoint.forward);
                _player = _spaceshipFactory.Create(_playerSpawnPoint.position, rotation);
                _player.Init(new InvulnerableState(), new DisabledMovingBehavior(), new PlayerShootingBehavior(InputHandler));
            }

            return _player;
        }

        public override void StartBattle()
        {
            StartCoroutine(StartCreatingEnemies());
        }

        private IEnumerator StartCreatingEnemies()
        {
            while (true)
            {
                var position = _enemySpawnPoint.position + new Vector3(
                    Random.Range(-_maxSpawnRange, _maxSpawnRange),
                    Random.Range(-_maxSpawnRange, _maxSpawnRange), 
                    Random.Range(-_maxSpawnRange, _maxSpawnRange));
                var rotation = Quaternion.LookRotation(-_player.transform.right);
                _enemy ??= _unfinishedSpaceshipFactory.Create(position, rotation);
                var hasCollision = Physics.CheckBox(position, _enemy.Size / 2, rotation);
                _enemy.gameObject.SetActive(!hasCollision);

                if (hasCollision)
                {
                    yield return null;
                }
                else
                {
                    var enemy = _enemy;
                    enemy.gameObject.layer = _enemyMask;
                    enemy.Init(new VulnerableState(), new MovingForwardBehavior(), new DisabledShootingBehavior());
                    enemy.transform.position = position;
                    _distanceDestructor.AddSpaceship(enemy);
                    _enemy = null;
                    
                    yield return _interval;
                }
            }
        }
    }
}