using System.Collections.Generic;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Spaceships.Services
{
    public class LaserBeamGunHolder : BaseWeaponHolder
    {
        [SerializeField] private List<LaserBeamGun> _weapons;
        
        public override void Init(DamageAgent damageAgent)
        {
            _damageAgent = damageAgent;
            
            foreach (var weapon in _weapons)
            {
                weapon.DamageAgent = _damageAgent;
                weapon.FiringRange = _firingRange;
            }
        }

        public override Vector3 CalculateFiringDirection(Transform target, float targetSpeed) => target.position;

        public override void Shoot(Vector3 targetPosition)
        {
            foreach (var weapon in _weapons)
            {
                if (weapon.IsReady)
                {
                    weapon.Shoot(targetPosition);
                }
            }
        }
    }
}