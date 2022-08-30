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
        [SerializeField] private Effect _destructionEffect;

        public List<Spaceship> Spaceships => _spaceships;
        public Effect DestructionEffect => _destructionEffect;
    }
}