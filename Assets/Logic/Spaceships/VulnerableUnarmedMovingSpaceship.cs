using Logic.Spaceships.Behaviors;
using UnityEngine;

namespace Logic.Spaceships
{
    public class VulnerableUnarmedMovingSpaceship : Spaceship
    {
        private Vector3 _startPosition;
        private float _destructionDistance;
        private bool _distanceDestructionIsEnabled;
        
        public float Speed { get; set; } = 100;

        protected void Update()
        {
            base.Update();

            if (_distanceDestructionIsEnabled && (transform.position - _startPosition).magnitude >= _destructionDistance)
            {
                Destroy(gameObject);
            }
        }

        public override void InitBehaviors()
        {
            _damageable = new VulnerableState(_durabilityPoints);
            _moveable = new MovingForwardBehavior(Speed);
            _shootable = new DisabledShootingBehavior();
            Init();
        }

        public void SetDestructionDistance(float distance)
        {
            _startPosition = transform.position;
            _destructionDistance = distance;
            _distanceDestructionIsEnabled = true;
        }
    }
}