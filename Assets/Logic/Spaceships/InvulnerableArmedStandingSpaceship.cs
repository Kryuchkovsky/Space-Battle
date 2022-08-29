using System;
using System.Collections.Generic;
using Cinemachine;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class InvulnerableArmedStandingSpaceship: Spaceship
    {
        public event Action OnClick;

        [SerializeField] private List<BaseWeaponHolder> _weaponHolders;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Canvas _interface;
        
        private int _weaponHolderIndex;
        public CinemachineVirtualCamera Camera => _camera;

        public void Init()
        {
            InitBehaviors();
            _interface.enabled = true;
            _inputHandler.SetStatus(true);
            _inputHandler.OnInput += Shoot;
        }

        private void Awake()
        {
            _interface.enabled = false;
            _inputHandler.SetStatus(false);
        }

        protected void OnDestroy()
        {
            base.OnDestroy();
            _inputHandler.OnInput -= Shoot;
        }

        protected override void InitBehaviors()
        {
            _damageable = new InvulnerableState();
            _moveable = new DisabledMovingBehavior();
            _shootable = new PlayerShootingBehavior();
        }

        public void NextWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == _weaponHolders.Count - 1 ? 0 : _weaponHolderIndex + 1;
        public void PreviousWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == 0 ? _weaponHolders.Count - 1 : _weaponHolderIndex - 1;

        private void Shoot(Ray ray) => _shootable.Shoot(_weaponHolders[_weaponHolderIndex], ray);

        private void OnMouseDown() => OnClick?.Invoke();
    }
}