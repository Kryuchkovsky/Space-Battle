using System;
using System.Collections.Generic;
using Cinemachine;
using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships
{
    public abstract class Spaceship : MonoBehaviour
    {
        public event Action OnClick;
        public event Action OnSpaceshipDestroy;

        [SerializeField] protected List<BaseWeaponHolder> _weaponHolders;
        [SerializeField] protected CinemachineVirtualCamera _camera;
        [SerializeField] protected MeshRenderer _meshRenderer;
        [SerializeField] protected DamageAgent _damageAgent;
        [SerializeField] [Min(0)] protected float _durabilityPoints = 500;

        protected IDamageable _damageable;
        protected IMoveable _moveable;
        protected IShootable _shootable;

        public Transform Target { get; set; }
        public CinemachineVirtualCamera Camera => _camera;
        public Vector3 Size => _meshRenderer.bounds.size;

        public abstract void InitBehaviors();

        protected void Init()
        {
            _damageable.OnDestroy += InvokeDestruction;
            _damageAgent.OnDamageTake += _damageable.TakeDamage;
        }

        protected void Update()
        {
            if (_moveable == null)
            {
                return;
            }
            
            _moveable.Move(this);
        }

        protected void OnDestroy()
        {
            _damageable.OnDestroy -= InvokeDestruction;
            _damageAgent.OnDamageTake -= _damageable.TakeDamage;
        }

        public void TakeDamage(float damage) => _damageable.TakeDamage(damage);

        protected void InvokeDestruction()
        {
            OnSpaceshipDestroy?.Invoke();
            Destroy(gameObject);
        }

        private void OnMouseDown() => OnClick?.Invoke();
    }
}