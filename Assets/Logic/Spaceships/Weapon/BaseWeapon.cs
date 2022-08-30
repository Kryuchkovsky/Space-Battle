using Logic.Spaceships.Services;
using Logic.Visual;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform _shotPoint;
        [SerializeField] protected Effect _hitEffectPrefab;
        [SerializeField] [Min(0)] protected float _damage = 50;
        [SerializeField] [Min(0)] protected float _reloadTime = 0.25f;

        public DamageAgent DamageAgent { get; set; }
        public float ReloadTime => _reloadTime;
        public float FiringRange { get; set; } = 10000;
        public bool IsReady { get; protected set; } = true;
        public abstract void Shoot(Vector3 endPoint);

        public bool CanHit(Vector3 endPoint)
        {
            var ray = new Ray(_shotPoint.position, (endPoint - _shotPoint.position).normalized * 100);
            var hasHit = Physics.Raycast(ray, out RaycastHit hit, FiringRange);
            var result = !hasHit || hit.collider.TryGetComponent(out DamageAgent agent) && agent != DamageAgent;
            
            return result;
        }
    }
}