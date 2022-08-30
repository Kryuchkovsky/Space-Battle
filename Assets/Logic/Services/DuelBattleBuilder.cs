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
        [SerializeField] private VulnerableArmedPursuingPlayerSpaceship _playerPrefab;
        [SerializeField] private VulnerableArmedPursuingSpaceship _enemyPrefab;

        private RandomSpaceshipFactory<VulnerableArmedPursuingPlayerSpaceship> _playerSpaceshipFactory;
        private RandomSpaceshipFactory<VulnerableArmedPursuingSpaceship> _enemiesSpaceshipFactory;
        private ObjectPool<Effect> _destructionEffectPool;
        private VulnerableArmedPursuingPlayerSpaceship _player;
        private VulnerableArmedPursuingSpaceship _enemy;
        
        public override void Init()
        {
            var players = new List<VulnerableArmedPursuingPlayerSpaceship>();
            var ememies = new List<VulnerableArmedPursuingSpaceship>();
            players.Add(_playerPrefab);
            ememies.Add(_enemyPrefab);
            _playerSpaceshipFactory = new RandomSpaceshipFactory<VulnerableArmedPursuingPlayerSpaceship>(players, transform);
            _enemiesSpaceshipFactory = new RandomSpaceshipFactory<VulnerableArmedPursuingSpaceship>(ememies, transform);
            _destructionEffectPool = new ObjectPool<Effect>(_data.DestructionEffect, transform);
        }

        public override Spaceship CreatePlayer()
        {
            if (!_player)
            {
                var rotation = Quaternion.LookRotation(_playerSpawnPoint.forward);
                _player = _playerSpaceshipFactory.Create(_playerSpawnPoint.position, rotation);
            }

            return _player;
        }

        public override void StartBattle()
        {
            _enemy = _enemiesSpaceshipFactory.Create(_enemySpawnPoint.position, Quaternion.LookRotation(_player.transform.forward));
            _enemy.InitBehaviors();
            _enemy.Target = _player.transform;
            _player.InitBehaviors();
            _player.Target = _enemy.transform;
            _player.OnSpaceshipDestroy += () => _destructionEffectPool.Take(_player.transform.position);
            _enemy.OnSpaceshipDestroy += () => _destructionEffectPool.Take(_enemy.transform.position);
        }
    }
}