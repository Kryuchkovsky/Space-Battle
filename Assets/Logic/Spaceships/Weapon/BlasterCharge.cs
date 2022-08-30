using System;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class BlasterCharge : MonoBehaviour
    {
        public event Action<BlasterCharge> OnDestruction;
        
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] [Min(0)] private float _speed = 100;

        private DamageAgent _damageAgent;
        private float _traveledDistance;
        private float _distance;
        private float _damage;
        private bool _isDestroyed;

        public void Init(DamageAgent damageAgent, float distance, float damage)
        {
            _damageAgent = damageAgent;
            _distance = distance;
            _damage = damage;
            _traveledDistance = 0;
            _isDestroyed = false;
        }

        private void Update()
        {
            if (_isDestroyed)
            {
                return;
            }
            
            FlyForward();
        }

        public void FlyForward()
        {
            var step = transform.forward * _speed * Time.deltaTime;
            transform.position += step;
            _traveledDistance += step.magnitude;

            if (_traveledDistance >= _distance)
            {
                PrepareToBeDestroyed();
            }
        }

        private void PrepareToBeDestroyed()
        {
            _trail.Clear();
            _isDestroyed = true;
            OnDestruction?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageAgent agent) && _damageAgent != agent)
            {
                agent.TakeDamage(_damage);
                PrepareToBeDestroyed();
            }
        }
    }
}