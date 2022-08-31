using System;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class BlasterCharge : MonoBehaviour
    {
        public event Action<BlasterCharge> Callback;
        
        [SerializeField] private TrailRenderer _trail;

        private DamageAgent _damageAgent;
        private float _traveledDistance;
        private float _distance = 10000;
        private float _damage = 100;
        private float _speed = 1500;

        public void Init(DamageAgent damageAgent, float distance, float damage, float speed)
        {
            _damageAgent = damageAgent;
            _distance = distance;
            _damage = damage;
            _speed = speed;
            _traveledDistance = 0;
        }

        private void Update()
        {
            FlyForward();
        }

        public void FlyForward()
        {
            var step = transform.forward * _speed * Time.deltaTime;
            transform.position += step;
            _traveledDistance += step.magnitude;

            if (_traveledDistance >= _distance)
            {
                Explode();
            }
        }

        private void Explode()
        {
            _trail.Clear();
            Callback?.Invoke(this);
            Callback = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DamageAgent agent) && _damageAgent != agent)
            {
                agent.TakeDamage(_damage);
                Explode();
            }
        }
    }
}