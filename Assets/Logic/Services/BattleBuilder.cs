using System;
using Logic.Data;
using Logic.Spaceships;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Services
{
    public class BattleBuilder : MonoBehaviour
    {
        [SerializeField] private LevelData _data;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _enemiesSpawnPoint;

        private RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship> _playerSpaceshipFactory;
        private RandomSpaceshipFactory<VulnerableUnarmedMovingSpaceship> _enemiesSpaceshipFactory;

        public void Init()
        {
            _playerSpaceshipFactory = new RandomSpaceshipFactory<InvulnerableArmedStandingSpaceship>(_data.PlayerSpaceships, transform);
            _enemiesSpaceshipFactory = new RandomSpaceshipFactory<VulnerableUnarmedMovingSpaceship>(_data.EnemySpaceships, transform);
        }

        public InvulnerableArmedStandingSpaceship CreatePlayer()
        {
            var player = _playerSpaceshipFactory.Create();
            player.transform.position = _playerSpawnPoint.position;
            player.transform.rotation = Quaternion.LookRotation(_playerSpawnPoint.forward);

            return player;
        }
        
        public VulnerableUnarmedMovingSpaceship CreateEnemy()
        {
            var enemy = _enemiesSpaceshipFactory.Create();
            enemy.transform.position = _enemiesSpawnPoint.position;
            enemy.transform.rotation = Quaternion.LookRotation(_enemiesSpawnPoint.forward);

            return enemy;
        }
    }
}