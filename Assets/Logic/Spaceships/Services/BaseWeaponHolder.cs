using UnityEngine;

namespace Logic.Spaceships.Services
{
    public abstract class BaseWeaponHolder : MonoBehaviour
    {
        [SerializeField] [Min(0)] protected float _reloadTime = 0.25f;
        [SerializeField] [Min(0)] protected float _damage = 100;
        [SerializeField] [Min(0)] protected float _firingRange = 10000;
        [SerializeField] protected bool _isTurret;
        
        protected DamageAgent _damageAgent;

        public bool IsTurret => _isTurret;
        
        public abstract void Init(DamageAgent damageAgent);
        public abstract void Shoot(Vector3 point);

        public abstract Vector3 CalculateFiringDirection(Transform target, float targetSpeed);
    }
}