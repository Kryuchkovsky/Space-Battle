using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class VulnerableUnarmedMovingSpaceship : Spaceship
    {
        [SerializeField] private DamageAgent _damageAgent;
        [SerializeField] [Min(0)] private float _durabilityPoints;

        private Vector3 _startPosition;
        private float _destructionDistance;
        private bool _distanceDestructionIsEnabled;
        
        public float Speed { get; set; } = 100;

        private void Awake()
        {
            InitBehaviors();
            _damageable.OnDestroy += InvokeDestruction;
            _damageAgent.OnDamageTake += _damageable.TakeDamage;
        }

        private void OnDestroy()
        {
            _damageable.OnDestroy -= InvokeDestruction;
            _damageAgent.OnDamageTake -= _damageable.TakeDamage;
        }

        private void Update()
        {
            _moveable.Move(this);

            if (_distanceDestructionIsEnabled && (transform.position - _startPosition).magnitude >= _destructionDistance)
            {
                Destroy(gameObject);
            }
        }

        protected override void InitBehaviors()
        {
            _damageable = new VulnerableState(_durabilityPoints);
            _moveable = new MovingForwardBehavior(Speed);
            _shootable = new DisabledShootingBehavior();
        }

        public void SetDestructionDistance(float distance)
        {
            _startPosition = transform.position;
            _destructionDistance = distance;
            _distanceDestructionIsEnabled = true;
        }
    }
}