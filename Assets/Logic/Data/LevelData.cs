using System.Collections.Generic;
using Logic.Spaceships;
using Logic.Visual;
using UnityEngine;

namespace Logic.Data
{
    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private List<Spaceship> _spaceships;
        [SerializeField] private List<Spaceship> _unfinishedSpaceships;

        public List<Spaceship> Spaceships => _spaceships;
        public List<Spaceship> UnfinishedSpaceships => _unfinishedSpaceships;
    }
}