using System.Collections;
using System.Collections.Generic;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Spaceships.Services
{
    public class LaserCannonHolder : BaseWeaponHolder
    {
        [SerializeField] private List<FastFiringLaserCannon> _weapons;
        
        private WaitForSeconds _weaponSwitching;
        private int _weaponIndex;
        private float _lastWeaponReload;
        private bool _canShoot = true;

        private void Awake()
        {
            _weaponSwitching = new WaitForSeconds(_lastWeaponReload);
        }

        public override void Shoot(Vector3 targetPosition)
        {
            var weapon = _weapons[_weaponIndex];
            
            if (!_canShoot || !weapon.CanShoot)
            {
                return;
            }
            
            weapon.Shoot(targetPosition);
            NextWeapon();

            if (_weapons[_weaponIndex].ReloadTime > 0)
            {
                if (_lastWeaponReload != weapon.ReloadTime / _weapons.Count)
                {
                    _lastWeaponReload = weapon.ReloadTime / _weapons.Count;
                    _weaponSwitching = new WaitForSeconds(_lastWeaponReload);
                }
                    
                StartCoroutine((IEnumerator) SwitchWeapon());
            }
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