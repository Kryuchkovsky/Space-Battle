using System;
using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships.Services
{
    public class DamageAgent : MonoBehaviour
    {
        public event Action<float> OnDamageTake;
        
        public void TakeDamage(float damage) => OnDamageTake?.Invoke(damage);
    }
}