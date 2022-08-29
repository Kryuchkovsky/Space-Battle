using System.Collections.Generic;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Spaceships.Services
{
    public class LaserBeamGunHolder : BaseWeaponHolder
    {
        [SerializeField] private List<LaserBeamGun> _weapons;
        
        public override void Shoot(Vector3 targetPosition)
        {
            foreach (var weapon in _weapons)
            {
                if (weapon.CanShoot)
                {
                    weapon.Shoot(targetPosition);
                }
            }
        }
    }
}