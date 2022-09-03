using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public struct ChargeData
    {
        public readonly Color Color;
        public readonly int SelfDestructDistance;
        public readonly int Damage;
        public readonly int Speed;

        public ChargeData(Color color, int selfDestructDistance, int damage, int speed)
        {
            Color = color;
            SelfDestructDistance = selfDestructDistance;
            Damage = damage;
            Speed = speed;
        }
    }
}