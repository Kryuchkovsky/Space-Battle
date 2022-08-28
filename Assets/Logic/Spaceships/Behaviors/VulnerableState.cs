using System;
using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class VulnerableState : IDamageable
    {
        public event Action OnDestroy;

        private bool _isDestroyed;
        
        public float DurabilityPoints { get; private set; }

        public VulnerableState(float durabilityPoints)
        {
            DurabilityPoints = durabilityPoints;
        }

        public void TakeDamage(float damage)
        {
            if (_isDestroyed)
            {
                return;
            }
            
            DurabilityPoints = Mathf.Clamp(DurabilityPoints - damage, 0, DurabilityPoints);
            _isDestroyed = DurabilityPoints == 0;
            
            if (_isDestroyed)
            {
                OnDestroy?.Invoke();
            }
        }
    }
}