using System;
using System.Collections.Generic;
using Cinemachine;
using Logic.Data;
using Logic.Services;
using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public class Spaceship : MonoBehaviour
    {
        [SerializeField] private List<BaseWeaponHolder> _weaponHolders;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private DamageAgent _damageAgent;
        [SerializeField] private TouchAgent _touchAgent;
        [SerializeField] [Min(0)] private float _durabilityPoints = 500;
        [SerializeField] [Min(0)] private float _movementSpeed = 120;
        [SerializeField] [Min(0)] private float _rotationSpeed = 60;

        private EffectManager _effectManager = EffectManager.Instance;
        private IDamageable _damageable;
        private IMoveable _moveable;
        private IShootable _shootable;
        private int _weaponHolderIndex;

        public Transform Target { get; set; }
        public TouchAgent TouchAgent => _touchAgent;
        public CinemachineVirtualCamera Camera => _camera;
        public Vector3 Size => _meshRenderer.bounds.size;
        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float CurrentDurability { get; set; }
        public bool HasVariousWeapons => _weaponHolders.Count > 1;

        public void Init(IDamageable damageable, IMoveable moveable, IShootable shootable)
        {
            _damageable = damageable;
            _moveable = moveable;
            _shootable = shootable;
            CurrentDurability = _durabilityPoints;
            
            foreach (var weaponHolder in _weaponHolders)
            {
                weaponHolder.Init(_damageAgent);
            }
            
            _damageAgent.OnDamageTake += TakeDamage;
        }

        private void OnDestroy()
        {
            _damageAgent.OnDamageTake -= TakeDamage;
        }

        private void Update()
        {
            _moveable?.Move(this);

            if (_weaponHolders != null && _weaponHolders.Count > 0)
            {
                _shootable?.Shoot(this, _weaponHolders[_weaponHolderIndex]);
            }
        }

        public void TakeDamage(float damage) => _damageable.TakeDamage(this, damage);
        public void NextWeapon() => _weaponHolderIndex = _weaponHolderIndex == _weaponHolders.Count - 1 ? 0 : _weaponHolderIndex + 1;
        public void PreviousWeapon() => _weaponHolderIndex = _weaponHolderIndex == 0 ? _weaponHolders.Count - 1 : _weaponHolderIndex - 1;

        public void Explode()
        {
            _effectManager.CreateEffectByType(EffectType.Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public enum SpaceshipType
    {
        MilleniumFalcon,
        SlaveOne,
        ThrantaClassCorvette,
        ValorClassCruiser
    }
}