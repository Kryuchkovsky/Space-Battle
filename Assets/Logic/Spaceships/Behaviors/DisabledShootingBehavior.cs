using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class DisabledShootingBehavior : IShootable
    {
        public void Shoot(BaseWeaponHolder weaponHolder, Ray ray)
        {
            //Do nothing
        }
    }
}
