using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class VulnerableState : IDamageable
    {
        public void TakeDamage(Spaceship spaceship, float damage)
        {
            spaceship.CurrentDurability = Mathf.Clamp(spaceship.CurrentDurability - damage, 0, spaceship.CurrentDurability);

            if (spaceship.CurrentDurability == 0)
            {
                spaceship.Explode();
            }
        }
    }
}