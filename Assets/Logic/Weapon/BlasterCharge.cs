using System;
using UnityEngine;

namespace Logic.Weapon
{
    public class BlasterCharge : MonoBehaviour
    {
        public event Action<BlasterCharge> OnDestruction;
        
        [SerializeField] [Min(0)] private float _speed = 100;
        
        private float _traveledDistance;
        private bool _isDestroyed;
        
        public float RangeOfFlight { get; set; }

        private void OnEnable()
        {
            _traveledDistance = 0;
            _isDestroyed = false;
        }

        private void Update()
        {
            if (_isDestroyed)
            {
                return;
            }
            
            FlyForward();
        }

        public void FlyForward()
        {
            var step = transform.forward * _speed * Time.deltaTime;
            transform.position += step;
            _traveledDistance += step.magnitude;

            if (_traveledDistance >= RangeOfFlight)
            {
                _isDestroyed = true;
                OnDestruction?.Invoke(this);
            }
        }
    }
}