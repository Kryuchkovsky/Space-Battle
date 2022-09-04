using Logic.Data;
using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class LaserBeamGun : BaseWeapon
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] [Min(0)] private float _turnOffDelay = 0.1f;
        [SerializeField] [Min(0)] private float _emissionIntensity = 20;

        private Effect _hitEffect;
        private float _delay;
        private bool _isInit;

        public override void Init(DamageAgent agent, WeaponData data)
        {
            base.Init(agent, data);
            _audioSource.clip = _data.AudioClip;
            _lineRenderer.material.color = _data.Color;
            _lineRenderer.material.SetColor("_EmissionColor", _data.Color * _emissionIntensity);
            _lineRenderer.startColor = _data.Color;
            _lineRenderer.endColor = _data.Color;
            _hitEffect = _effectManager.CreateEffectByType(EffectType.Melting, transform.position, Quaternion.identity);
            _isInit = true;
        }

        private void Update()
        {
            if (!_isInit)
            {
                return;
            }
            
            _lineRenderer.enabled = _delay > 0;

            if (_lineRenderer.enabled)
            {
                _delay = Mathf.Clamp(_delay - Time.deltaTime, 0, _turnOffDelay);
                _lineRenderer.SetPosition(0, _shotPoint.position);
            }
            else
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
                
                _hitEffect.gameObject.SetActive(false);
            }
        }

        public override void Shoot(Vector3 endPoint)
        {
            var ray = new Ray(_shotPoint.position, endPoint - _shotPoint.position);
            var hasHit = Physics.Raycast(ray, out RaycastHit hit);

            if (hasHit)
            {
                if (hit.collider.TryGetComponent(out DamageAgent agent))
                {
                    agent.TakeDamage(_data.Damage * Time.deltaTime);
                }

                _hitEffect.transform.position = hit.point;
                _hitEffect.transform.rotation = Quaternion.LookRotation(-ray.direction);
            }

            _delay = _turnOffDelay;
            _lineRenderer.SetPosition(1,  endPoint);
            _hitEffect.gameObject.SetActive(hasHit);

            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }
}