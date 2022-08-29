using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class LaserBeamGun : BaseWeapon
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] [Min(0)] private float _turnOffDelay = 0.25f;

        private Effect _hitEffect;
        private float _delay;

        private void Awake()
        {
            _hitEffect = Instantiate(_hitEffectPrefab, transform);
        }

        private void Update()
        {
            _delay = Mathf.Clamp(_delay - Time.deltaTime, 0, _turnOffDelay);
            _lineRenderer.enabled = _delay > 0;

            if (!_lineRenderer.enabled)
            {
                if (_audioSource.isPlaying)
                {
                    _audioSource.Stop();
                }
                
                if (_hitEffect.ParticleSystem.isPlaying)
                {
                    _hitEffect.ParticleSystem.Stop();
                }
            }
        }

        public override void Shoot(Vector3 endPoint)
        {
            var ray = new Ray(_shotPoint.position, endPoint - _shotPoint.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out DamageAgent agent))
                {
                    agent.TakeDamage(_damage * Time.deltaTime);
                }

                _hitEffect.transform.position = hit.point;
                _hitEffect.transform.rotation = Quaternion.LookRotation(-ray.direction);
                
                if (!_hitEffect.ParticleSystem.isPlaying)
                {
                    _hitEffect.ParticleSystem.Play();
                }
            }
            else if (_hitEffect.ParticleSystem.isPlaying)
            {
                _hitEffect.ParticleSystem.Stop();
            }

            _delay = _turnOffDelay;
            _lineRenderer.SetPosition(0, _shotPoint.position);
            _lineRenderer.SetPosition(1,  endPoint);


            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            
        }
    }
}