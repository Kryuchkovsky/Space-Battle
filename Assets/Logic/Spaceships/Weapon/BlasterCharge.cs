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
        
        private float _traveledDistance;
        private bool _isDestroyed;
        
        public float Damage { get; set; }
        public float RangeOfFlight { get; set; }

        private void OnEnable()
        {
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

            if (_traveledDistance >= RangeOfFlight)
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
            if (other.TryGetComponent(out DamageAgent agent))
            {
                agent.TakeDamage(Damage);
                PrepareToBeDestroyed();
            }
        }
    }
}