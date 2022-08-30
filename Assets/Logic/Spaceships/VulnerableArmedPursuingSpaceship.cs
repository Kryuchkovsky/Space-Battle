using System;
using System.Collections.Generic;
using Cinemachine;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class VulnerableArmedPursuingSpaceship: Spaceship
    {
        [SerializeField] private List<BaseWeaponHolder> _weaponHolders;

        [SerializeField] [Min(0)] private float _durabilityPoints;
        
        private int _weaponHolderIndex;

        public void Init(Transform target)
        {
            Target = target;
            InitBehaviors();
        }

        private void Update()
        {
            if (Target)
            {
                _moveable.Move(this);

                if (transform.forward == (Target.position - transform.position).normalized)
                { 
                    _shootable.Shoot( _weaponHolders[_weaponHolderIndex], new Ray(transform.position, Target.position - transform.position));
                }
            }
        }

        protected override void InitBehaviors()
        {
            _damageable = new VulnerableState(_durabilityPoints);
            _moveable = new PursuingBehavior();
            _shootable = new BotShootingBehavior(this);
        }

        public void NextWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == _weaponHolders.Count - 1 ? 0 : _weaponHolderIndex + 1;
        public void PreviousWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == 0 ? _weaponHolders.Count - 1 : _weaponHolderIndex - 1;

        private void Shoot(Ray ray) => _shootable.Shoot(_weaponHolders[_weaponHolderIndex], ray);
    }
}