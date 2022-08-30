using System;
using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships
{
    public abstract class Spaceship : MonoBehaviour
    {
        public event Action OnSpaceshipDestroy;
        
        [SerializeField] protected MeshRenderer _meshRenderer;
        
        protected IDamageable _damageable;
        protected IMoveable _moveable;
        protected IShootable _shootable;

        public Transform Target { get; set; }
        public Vector3 Size => _meshRenderer.bounds.size;
        
        protected abstract void InitBehaviors();
        
        public void TakeDamage(float damage) => _damageable.TakeDamage(damage);

        protected void InvokeDestruction()
        {
            OnSpaceshipDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}