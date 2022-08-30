using Logic.Spaceships.Services;

namespace Logic.Spaceships.Interfaces
{
    public interface IShootable
    {
        public void Shoot(Spaceship spaceship, BaseWeaponHolder weaponHolder);
    }
}
