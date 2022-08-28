using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships
{
    public abstract class Spaceship : MonoBehaviour
    {
        protected IDamageable _damageable;
        protected IMoveable _moveable;
        protected IShootable _shootable;

        protected abstract void InitBehaviors();
        
        public void TakeDamage(float damage) => _damageable.TakeDamage(damage);
    }
}