using System;

namespace Logic.Spaceships.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(Spaceship spaceship, float damage);
    }
}
