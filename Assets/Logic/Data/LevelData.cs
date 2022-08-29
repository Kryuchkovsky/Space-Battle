using System.Collections.Generic;
using Logic.Spaceships;
using Logic.Visual;
using UnityEngine;

namespace Logic.Data
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<VulnerableUnarmedMovingSpaceship> _enemySpaceships;
        [SerializeField] private List<InvulnerableArmedStandingSpaceship> _playerSpaceships;
        [SerializeField] private Effect _destructionEffect;
        [SerializeField] [Min(0)] private float _maxSpawnRange = 250;
        [SerializeField] [Min(0)] private float _spawnInterval = 5;

        public List<VulnerableUnarmedMovingSpaceship> EnemySpaceships => _enemySpaceships;
        public List<InvulnerableArmedStandingSpaceship> PlayerSpaceships => _playerSpaceships;
        public Effect DestructionEffect => _destructionEffect;
        public float MaxSpawnRange => _maxSpawnRange;
        public float SpawnInterval => _spawnInterval;
    }
}