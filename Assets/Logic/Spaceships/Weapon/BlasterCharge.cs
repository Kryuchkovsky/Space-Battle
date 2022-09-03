using System;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class BlasterCharge : MonoBehaviour
    {
        public event Action<BlasterCharge> Callback;
        
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] [Min(0)] private float _emissionIntensity = 20;

        private DamageAgent _damageAgent;
        private WeaponData _data;
        private float _traveledDistance;

        public void Init(DamageAgent damageAgent, WeaponData data)
        {
            _damageAgent = damageAgent;
            _data = data;
            _meshRenderer.material.color = _data.Color;
            _meshRenderer.material.SetColor("_EmissionColor", _data.Color * _emissionIntensity);
            _trail.material.color = _data.Color;
            _trail.material.SetColor("_EmissionColor", _data.Color * _emissionIntensity);
            _trail.startColor = _data.Color;
            _trail.endColor = _data.Color;
            _traveledDistance = 0;
        }

        private void Update()
        {
            FlyForward();
        }

        public void FlyForward()
        {
            var step = transform.forward * _data.ChargeSpeed * Time.deltaTime;
            transform.position += step;
            _traveledDistance += step.magnitude;

            if (_traveledDistance >= _data.FiringRange)
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
                agent.TakeDamage(_data.Damage);
                Explode();
            }
        }
    }
}