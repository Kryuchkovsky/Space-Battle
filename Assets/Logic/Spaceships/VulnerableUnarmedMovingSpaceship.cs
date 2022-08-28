using System;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class VulnerableUnarmedMovingSpaceship : Spaceship
    {
        [SerializeField] private DamageAgent _damageAgent;
        [SerializeField] [Min(0)] private float _speed;
        [SerializeField] [Min(0)] private float _durabilityPoints;
        
        private void Awake()
        {
            InitBehaviors();
            _damageable.OnDestroy += () => Destroy(gameObject);
            _damageAgent.OnDamageTake += _damageable.TakeDamage;
        }

        protected void OnDestroy()
        {
            base.OnDestroy();
            _damageAgent.OnDamageTake -= _damageable.TakeDamage;
        }

        private void Update()
        {
            _moveable.Move(this);
        }

        protected override void InitBehaviors()
        {
            _damageable = new VulnerableState(_durabilityPoints);
            _moveable = new MovingForwardBehavior(_speed);
            _shootable = new DisabledShootingBehavior();
        }
    }
}