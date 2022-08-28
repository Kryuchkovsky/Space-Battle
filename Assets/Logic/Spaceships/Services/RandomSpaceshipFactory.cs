using System.Collections.Generic;
using UnityEngine;

namespace Logic.Spaceships
{
    public class RandomSpaceshipFactory<T> : AbstractFactory<T> where T : Spaceship
    {
        private List<T> _prefabs;
        private Transform _parent;

        public RandomSpaceshipFactory(List<T> prefabs, Transform parent)
        {
            _prefabs = prefabs;
            _parent = parent;
        }

        public override T Create()
        {
            var prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            return Object.Instantiate(prefab, _parent);
        }
    }
}