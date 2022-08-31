using Logic.Spaceships.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Spaceships.Behaviors
{
    public class PursuingBehavior : IMoveable
    {
        private const float MAX_DISTANCE_FACTOR = 6;
        private const float MIN_ANGLE_FOR_GOING_AWAY = 150;

        private readonly float _minDistance;
        private readonly float _maxDistance;
        
        private Vector2 _randomDirection;
        private Quaternion _rotation;

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
            var forwardAngle = Vector3.Angle(spaceship.transform.forward, spaceship.Target.transform.forward);
            var distance = Vector3.Distance(spaceship.transform.position, spaceship.Target.transform.position);
            direction = distance > _maxDistance || distance > _minDistance && angle < spaceship.RotationSpeed || forwardAngle < spaceship.RotationSpeed
                ? direction
                : -direction;
            _rotation = Quaternion.LookRotation(direction);
            _rotation = Quaternion.RotateTowards(spaceship.transform.rotation, _rotation, spaceship.RotationSpeed * Time.deltaTime);
            spaceship.transform.rotation = _rotation;
        }
    }
}