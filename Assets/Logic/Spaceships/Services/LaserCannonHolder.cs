using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Spaceships.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Logic.Spaceships.Services
{
    public class LaserCannonHolder : BaseWeaponHolder
    {
        public override event Action OnShoot;
        
        [SerializeField] private List<FastFiringLaserCannon> _weapons;
        [SerializeField] private float _scatterFactor = 0.01f;

        private WaitForSeconds _weaponSwitching;
        private int _weaponIndex;
        private float _lastWeaponReload;
        private bool _canShoot = true;


        public override void Init(DamageAgent damageAgent)
        {
            _damageAgent = damageAgent;
            _lastWeaponReload = _data.ReloadTime / _weapons.Count;
            _weaponSwitching = new WaitForSeconds(_lastWeaponReload);
            
            foreach (var weapon in _weapons)
            {
                weapon.Init(_damageAgent, _data);
            }
        }

        public override Vector3 CalculateFiringDirection(Transform target, float targetSpeed)
        {
            var time = (target.position - transform.position).magnitude / _data.ChargeSpeed;
            var step = target.forward * targetSpeed * time;
            var distance = (target.position + step - transform.position).magnitude;
            time = distance / _data.ChargeSpeed;
            return target.position + step + target.forward * targetSpeed * time;
        }

        public override void Shoot(Vector3 point)
        {
            var weapon = _weapons[_weaponIndex];
            
            if (!_canShoot || !weapon.IsReady)
            {
                return;
            }
            
            if (weapon.CanHit(point))
            {
                var offset = (point - transform.position).magnitude * _scatterFactor;
                var scatter = transform.forward * Random.Range(-offset, offset) + transform.right * Random.Range(-offset, offset);
                weapon.Shoot(point + scatter);
                OnShoot?.Invoke();
                
                if (_data.ReloadTime > 0)
                {
                    StartCoroutine((IEnumerator) SwitchWeapon());
                }
            }
            
            NextWeapon();
        }

        private void NextWeapon() => _weaponIndex = _weaponIndex == _weapons.Count - 1 ? 0 : _weaponIndex + 1;

        private IEnumerator SwitchWeapon()
        {
            _canShoot = false;
            yield return _weaponSwitching;
            _canShoot = true;
        }
    }
}