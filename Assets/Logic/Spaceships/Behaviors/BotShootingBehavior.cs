using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class BotShootingBehavior : IShootable
    {
        public const float MIN_ANGLE = 5;

        public void Shoot(Spaceship spaceship, BaseWeaponHolder weaponHolder)
        {
            if (!spaceship.Target)
            {
                return;
            }
            
            var angle = Vector3.Angle(spaceship.transform.forward, spaceship.Target.transform.position - spaceship.transform.position);
            
            if (weaponHolder.IsTurret || angle < MIN_ANGLE)
            {
                var point = weaponHolder.CalculateFiringDirection(spaceship.Target.transform, spaceship.Target.MovementSpeed);
                weaponHolder.Shoot(point);
            }
        }
    }
}