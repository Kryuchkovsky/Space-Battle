using System.Collections.Generic;
using Cinemachine;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class InvulnerableArmedPursuingSpaceship: Spaceship
    {
        [SerializeField] private List<BaseWeaponHolder> _weaponHolders;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Canvas _interface;
        
        private int _weaponHolderIndex;

        public void Init()
        {
            InitBehaviors();
            _interface.enabled = true;
        }

        private void Awake()
        {
            _interface.enabled = false;
        }

        protected override void InitBehaviors()
        {
            _damageable = new InvulnerableState();
            _moveable = new PursuingBehavior();
            _shootable = new PlayerShootingBehavior();
        }

        public void NextWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == _weaponHolders.Count - 1 ? 0 : _weaponHolderIndex + 1;
        public void PreviousWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == 0 ? _weaponHolders.Count - 1 : _weaponHolderIndex - 1;

        private void Shoot(Ray ray) => _shootable.Shoot(_weaponHolders[_weaponHolderIndex], ray);
    }
}