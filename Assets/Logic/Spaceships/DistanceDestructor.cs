using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Spaceships
{
    public class DistanceDestructor: MonoBehaviour
    {
        [SerializeField] [Min(0)] private float _destructionDistance = 3000;
        
        private Dictionary<Spaceship, Vector3> _spaceships;

        private void Awake()
        {
            _spaceships = new Dictionary<Spaceship, Vector3>();
        }

        private void Update()
        {
            for (int i = 0; i < _spaceships.Count; i++)
            {
                var spaceship = _spaceships.ElementAt(i);

                if (spaceship.Key == null)
                {
                    _spaceships.Remove(spaceship.Key);
                    continue;
                }
                
                if ((spaceship.Value - spaceship.Key.transform.position).magnitude >= _destructionDistance)
                {
                    _spaceships.Remove(spaceship.Key);
                    Destroy(spaceship.Key.gameObject);
                }
            }
        }

        public void AddSpaceship(Spaceship spaceship) => _spaceships.Add(spaceship, spaceship.transform.position);
    }
}