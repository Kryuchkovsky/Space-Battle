using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;

namespace Logic.Spaceships.Behaviors
{
    public class DisabledShootingBehavior : IShootable
    {
        public void Shoot(Spaceship spaceship, BaseWeaponHolder weaponHolder)
        {
            //Do nothing
        }
    }
}
