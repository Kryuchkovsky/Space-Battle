using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public struct WeaponData
    {
        public readonly Color Color;
        public readonly float Damage;
        public readonly float FiringRange;
        public readonly float ReloadTime;
        public readonly float Speed;

        public WeaponData(Color color, float firingRange, float damage, float speed, float reloadTime)
        {
            Color = color;
            Damage = damage;
            FiringRange = firingRange;
            ReloadTime = reloadTime;
            Speed = speed;
        }
    }
}