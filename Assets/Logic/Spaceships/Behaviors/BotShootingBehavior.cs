using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class BotShootingBehavior : IShootable
    {
        private const float MAX_RAY_DISTANCE = 5000;
        private readonly Spaceship _spaceship;

        public BotShootingBehavior(Spaceship spaceship)
        {
            _spaceship = spaceship;
        }
        
        public void Shoot(BaseWeaponHolder weaponHolder, Ray ray)
        {
            weaponHolder.Shoot(_spaceship.Target.position);
        }
    }
}