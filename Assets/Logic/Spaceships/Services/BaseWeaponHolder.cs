using System;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Spaceships.Services
{
    public abstract class BaseWeaponHolder : MonoBehaviour
    {
        public abstract event Action OnShoot;
        
        [SerializeField] protected WeaponData _data;
        [SerializeField] protected bool _isTurret;
        
        protected DamageAgent _damageAgent;

        public bool IsTurret => _isTurret;
        
        public abstract void Init(DamageAgent damageAgent);
        public abstract void Shoot(Vector3 point);

        public abstract Vector3 CalculateFiringDirection(Transform target, float targetSpeed);
    }
}