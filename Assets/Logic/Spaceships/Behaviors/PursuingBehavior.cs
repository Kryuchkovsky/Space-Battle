using Logic.Spaceships.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Spaceships.Behaviors
{
    public class PursuingBehavior : IMoveable
    {
        private const float MAX_AXIAL_OFFSET = 0.5f;
        private const float MAX_DISTANCE_FACTOR = 6;
        private const float MIN_ANGLE_FOR_GOING_AWAY = 30;

        private readonly float _minDistance;
        private readonly float _maxDistance;
        
        private Vector2 _randomDirection;
        private Vector3 _up;
        private Quaternion _rotation;
        private bool _isGoindAway;

        public PursuingBehavior(Spaceship spaceship)
        {
            _minDistance = 180 / spaceship.RotationSpeed * spaceship.MovementSpeed;
            _maxDistance = _minDistance * MAX_DISTANCE_FACTOR;
        }
        
        public void Move(Spaceship spaceship)
        {
            spaceship.transform.position += spaceship.transform.forward * spaceship.MovementSpeed * Time.deltaTime;

            if (!spaceship.Target)
            {
                return;
            }
            
            var direction = (spaceship.Target.transform.position - spaceship.transform.position).normalized;
            var angle = Vector3.Angle(spaceship.transform.forward, direction);
            var distance = Vector3.Distance(spaceship.transform.position, spaceship.Target.transform.position);

            if (distance < _minDistance || distance < _maxDistance && angle > MIN_ANGLE_FOR_GOING_AWAY)
            {
                var forward = -direction + spaceship.transform.forward * _randomDirection.x + spaceship.transform.right * _randomDirection.y;
                var offset = spaceship.transform.up + spaceship.transform.right;
                _up = new Vector3(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y), Random.Range(-offset.z, offset.z));
                _rotation = Quaternion.LookRotation(forward);
                _isGoindAway = true;
            }
            else
            {
                _rotation = Quaternion.LookRotation(direction, _up);

                if (_isGoindAway)
                {
                    _isGoindAway = false;
                    _randomDirection = new Vector2(Random.Range(-MAX_AXIAL_OFFSET, MAX_AXIAL_OFFSET), Random.Range(-MAX_AXIAL_OFFSET, MAX_AXIAL_OFFSET));
                }
            }

            _rotation = Quaternion.RotateTowards(spaceship.transform.rotation, _rotation, spaceship.RotationSpeed * Time.deltaTime);
            spaceship.transform.rotation = _rotation;
        }
    }
}