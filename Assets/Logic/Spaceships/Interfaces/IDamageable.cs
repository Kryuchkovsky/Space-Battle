using System;

namespace Logic.Spaceships.Interfaces
{
    public interface IDamageable
    {
        public event Action OnDestroy;
        public float DurabilityPoints { get; }
        public void TakeDamage(float damage);
    }
}
