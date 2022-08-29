using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class PlayerShootingBehavior : IShootable
    {
        private const float MAX_RAY_DISTANCE = 5000;

        public void Shoot(BaseWeaponHolder weaponHolder, Ray ray)
        {
            var targetPosition = Physics.Raycast(ray, out RaycastHit hit)
                ? hit.point
                : Camera.main.transform.position + ray.direction * MAX_RAY_DISTANCE;
            weaponHolder.Shoot(targetPosition);
        }
    }
}