using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class DisabledShootingBehavior : IShootable
    {
        public void Shoot(Ray ray)
        {
            //Do nothing
        }
    }
}
