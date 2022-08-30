using UnityEngine;

namespace Logic.Spaceships.Services
{
    public abstract class BaseWeaponHolder : MonoBehaviour
    {
        [SerializeField] protected float _firingRange = 10000;
        [SerializeField] protected bool _isTurret;
        
        protected DamageAgent _damageAgent;

        public bool IsTurret => _isTurret;
        
        public abstract void Init(DamageAgent damageAgent);
        public abstract void Shoot(Vector3 targetPosition);
    }
}