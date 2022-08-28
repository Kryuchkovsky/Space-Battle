using System.Collections.Generic;
using Logic.Spaceships;
using UnityEngine;

namespace Logic.Data
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<VulnerableUnarmedMovingSpaceship> _enemySpaceships;
        [SerializeField] private List<InvulnerableArmedStandingSpaceship> _playerSpaceships;

        public List<VulnerableUnarmedMovingSpaceship> EnemySpaceships => _enemySpaceships;
        public List<InvulnerableArmedStandingSpaceship> PlayerSpaceships => _playerSpaceships;
    }
}