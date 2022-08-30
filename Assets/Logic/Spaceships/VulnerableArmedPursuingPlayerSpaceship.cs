using System.Collections.Generic;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class VulnerableArmedPursuingPlayerSpaceship: Spaceship
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Canvas _interface;

        private int _weaponHolderIndex;

        protected void Awake()
        {
            _interface.enabled = false;
            _inputHandler.SetStatus(false);
        }

        protected void OnDestroy()
        {
            base.OnDestroy();
            _inputHandler.OnInput -= Shoot;
        }

        public override void InitBehaviors()
        {
            _damageable = new VulnerableState(_durabilityPoints);
            _moveable = new PursuingBehavior();
            _shootable = new PlayerShootingBehavior();
            _interface.enabled = true;
            _inputHandler.SetStatus(true);
            _inputHandler.OnInput += Shoot;
            Init();
        }

        public void NextWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == _weaponHolders.Count - 1 ? 0 : _weaponHolderIndex + 1;
        public void PreviousWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == 0 ? _weaponHolders.Count - 1 : _weaponHolderIndex - 1;
        private void Shoot(Ray ray) => _shootable.Shoot(_weaponHolders[_weaponHolderIndex], ray);
    }
}