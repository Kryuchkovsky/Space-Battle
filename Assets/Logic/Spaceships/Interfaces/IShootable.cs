using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Interfaces
{
    public interface IShootable
    {
        public void Shoot(BaseWeaponHolder weaponHolder, Ray ray);
    }
}
