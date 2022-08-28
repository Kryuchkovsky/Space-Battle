using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class PlayerShootingBehavior : IShootable
    {
        private const float MAX_RAY_DISTANCE = 5000;
        
        private WeaponHolder _weaponHolder;

        public PlayerShootingBehavior(WeaponHolder weaponHolder)
        {
            _weaponHolder = weaponHolder;
        }
        
        public void Shoot(Ray ray)
        {
            var targetPosition = Physics.Raycast(ray, out RaycastHit hit)
                ? hit.point
                : Camera.main.transform.position + ray.direction * MAX_RAY_DISTANCE;
            _weaponHolder.Shoot(targetPosition);
        }
    }
}