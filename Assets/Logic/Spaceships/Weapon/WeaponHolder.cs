using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private List<BaseWeapon> _weapons;

        private List<BaseWeapon> _selectedWeapons;
        private List<WeaponType> _weaponTypes;
        private WaitForSeconds _weaponSwitching;
        private int _weaponTypeIndex;
        private int _weaponIndex;
        private float _lastWeaponReload;
        private bool _canShoot = true;
        private WeaponType _selectedWeaponType;

        private void Awake()
        {
            _weaponTypes = _weapons.Select(s => s.Type).Distinct().ToList();
            _selectedWeaponType = _weaponTypes[_weaponTypeIndex];
            SelectWeapon();
        }

        public void NextWeapon()
        {
            _weaponTypeIndex = _weaponTypeIndex == _weaponTypes.Count - 1 ? 0 : _weaponTypeIndex + 1;
            SelectWeapon();
        }

        public void PreviousWeapon()
        {
            _weaponTypeIndex = _weaponTypeIndex == 0 ? _weaponTypes.Count - 1 : _weaponTypeIndex - 1;
            SelectWeapon();
        }
        
        public void NextWeaponFromSelected() => _weaponIndex = _weaponIndex == _selectedWeapons.Count - 1 ? 0 : _weaponIndex + 1;

        public void Shoot(Vector3 targetPosition)
        {
            var weapon = _selectedWeapons[_weaponIndex];
            
            if (!_canShoot || !weapon.CanShoot)
            {
                return;
            }
            
            weapon.Shoot(targetPosition);
            NextWeaponFromSelected();

            if (weapon.ReloadTime > 0)
            {
                if (_lastWeaponReload != weapon.ReloadTime)
                {
                    _lastWeaponReload = weapon.ReloadTime;
                    _weaponSwitching = new WaitForSeconds(_lastWeaponReload);
                }
                    
                StartCoroutine(SwitchWeapon());
            }
        }

        private IEnumerator SwitchWeapon()
        {
            _canShoot = false;
            yield return _weaponSwitching;
            _canShoot = true;
        }

        private void SelectWeapon()
        {
            _weaponIndex = 0;
            _selectedWeaponType = _weaponTypes[_weaponTypeIndex];
            _selectedWeapons = _weapons.Where(w => w.Type == _selectedWeaponType).ToList();
        }
    }
}