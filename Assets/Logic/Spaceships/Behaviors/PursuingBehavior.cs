using Logic.Spaceships.Interfaces;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Spaceships.Behaviors
{
    public class PursuingBehavior : IMoveable
    {
        private Vector2 _randomDirection;
        private Vector3 _up;
        private Quaternion _rotation;
        private float _movementSpeed = 200;
        private float _rotationSpeed = 60;
        private float _minDistance = 200;
        private float _maxDistance = 700;
        private bool _isGoindAway;

        public void Move(Spaceship spaceship)
        {
            spaceship.transform.position += spaceship.transform.forward * _movementSpeed * Time.deltaTime;

            if (!spaceship.Target)
            {
                return;
            }
            
            var direction = (spaceship.Target.position - spaceship.transform.position).normalized;
            var angle = Vector3.Angle(spaceship.transform.forward, direction);
            var distance = Vector3.Distance(spaceship.transform.position, spaceship.Target.position);

            if (distance < _minDistance || distance < _maxDistance && angle > 30)
            {
                var look = -direction + spaceship.transform.forward * _randomDirection.x + spaceship.transform.right * _randomDirection.y;
                var temp = spaceship.transform.up + spaceship.transform.right;
                _up = new Vector3(Random.Range(-temp.x, temp.x), Random.Range(-temp.y, temp.y), Random.Range(-temp.z, temp.z));
                var to = Quaternion.LookRotation(look);
                _rotation = Quaternion.RotateTowards(spaceship.transform.rotation, to, _rotationSpeed * Time.deltaTime);
                _isGoindAway = false;
            }
            else
            {
                _rotation = Quaternion.RotateTowards(spaceship.transform.rotation, Quaternion.LookRotation(direction, _up), _rotationSpeed * Time.deltaTime);

                if (!_isGoindAway)
                {
                    _isGoindAway = true;
                    _randomDirection = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
                }
                
            }

            spaceship.transform.rotation = _rotation;
        }
    }
}
