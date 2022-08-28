using System;
using Cinemachine;
using Logic.Spaceships.Behaviors;
using Logic.Spaceships.Services;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Spaceships
{
    public class InvulnerableArmedStandingSpaceship : Spaceship
    {
        public event Action OnClick;

        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Canvas _interface;

        public int CameraPriority
        {
            get => _camera.Priority;
            set => _camera.Priority = value;
        }

        public void Init()
        {
            InitBehaviors();
            _interface.enabled = true;
            _inputHandler.SetStatus(true);
            _inputHandler.OnInput += _shootable.Shoot;
        }

        private void Awake()
        {
            _interface.enabled = false;
            _inputHandler.SetStatus(false);
        }

        protected void OnDestroy()
        {
            base.OnDestroy();
            _inputHandler.OnInput -= _shootable.Shoot;
        }

        protected override void InitBehaviors()
        {
            _damageable = new InvulnerableState();
            _moveable = new DisabledMovingBehavior();
            _shootable = new PlayerShootingBehavior(_weaponHolder);
        }

        private void OnMouseDown() => OnClick?.Invoke();
    }
}