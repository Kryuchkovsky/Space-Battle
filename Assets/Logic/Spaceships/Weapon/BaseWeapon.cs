using Logic.Services;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected AudioSource _audioSource;
        [SerializeField] protected Transform _shotPoint;
        
        protected EffectManager _effectManager = EffectManager.Instance;
        protected DamageAgent _agent;
        protected WeaponData _data;

        public bool IsReady { get; protected set; } = true;
        
        public abstract void Shoot(Vector3 endPoint);

        public virtual void Init(DamageAgent agent, WeaponData data)
        {
            _agent = agent;
            _data = data;
        }

        public bool CanHit(Vector3 endPoint)
        {
            var ray = new Ray(_shotPoint.position, (endPoint - _shotPoint.position).normalized * 100);
            var hasHit = Physics.Raycast(ray, out RaycastHit hit, _data.FiringRange);
            var canHit = !hasHit || hit.collider.TryGetComponent(out DamageAgent agent) && agent != _agent || agent == null;
            
            return canHit;
        }
    }
}