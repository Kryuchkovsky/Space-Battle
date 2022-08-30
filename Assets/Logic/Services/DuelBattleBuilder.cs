using System.Collections.Generic;
using Logic.Data;
using Logic.Patterns;
using Logic.Spaceships;
using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Services
{
    public class DuelBattleBuilder : BaseBattleBuilder
    {
        [SerializeField] private LevelData _data;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private VulnerableArmedPursuingSpaceship _enemyPrefab;

        private RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship> _playerSpaceshipFactory;
        private RandomSpaceshipFactory<VulnerableArmedPursuingSpaceship> _enemiesSpaceshipFactory;
        private ObjectPool<Effect> _destructionEffectPool;
        private InvulnerableArmedStandingSpaceship _player;
        private VulnerableArmedPursuingSpaceship _enemy;
        public override void Init()
        {
            var list = new List<VulnerableArmedPursuingSpaceship>();
            list.Add(_enemyPrefab);
            _playerSpaceshipFactory = new RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship>(_data.PlayerSpaceships, transform);
            _enemiesSpaceshipFactory = new RandomSpaceshipFactory<VulnerableArmedPursuingSpaceship>(list, transform);
            _destructionEffectPool = new ObjectPool<Effect>(_data.DestructionEffect, transform);
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
            _enemy = _enemiesSpaceshipFactory.Create(_enemySpawnPoint.position, Quaternion.LookRotation(_player.transform.forward));
            _enemy.Init(_player.transform);
            _enemy.OnSpaceshipDestroy += () => _destructionEffectPool.Take(_enemy.transform.position);
        }
    }
}