using Logic.Spaceships.Interfaces;

namespace Logic.Spaceships.Behaviors
{
    public class InvulnerableState : IDamageable
    {
        public void TakeDamage(Spaceship spaceship, float damage)
        {
            //Do nothing
        }
    }
}