using Logic.Services;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform _shotPoint;

        protected EffectManager _effectManager = EffectManager.Instance;

        public DamageAgent DamageAgent { get; set; }
        public float Damage { get; set; } = 100;
        public float ReloadTime { get; set; } = 0.25f;
        public float FiringRange { get; set; } = 10000;
        public bool IsReady { get; protected set; } = true;
        public abstract void Shoot(Vector3 endPoint);
        

        public bool CanHit(Vector3 endPoint)
        {
            var ray = new Ray(_shotPoint.position, (endPoint - _shotPoint.position).normalized * 100);
            var hasHit = Physics.Raycast(ray, out RaycastHit hit, FiringRange);
            var result = !hasHit || hit.collider.TryGetComponent(out DamageAgent agent) && agent != DamageAgent || agent == null;
            
            return result;
        }
    }
}