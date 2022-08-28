using System;
using Logic.Spaceships.Interfaces;

namespace Logic.Spaceships.Behaviors
{
    public class InvulnerableState : IDamageable
    {
        public event Action OnDestroy;

        public float DurabilityPoints => float.MaxValue;

        public void TakeDamage(float damage)
        {
            //Do nothing
        }
    }
}