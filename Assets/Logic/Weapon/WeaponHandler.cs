using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.Weapon
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private List<BaseWeapon> _weapons;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private LayerMask _mask;

        private List<BaseWeapon> _selectedWeapons;
        private List<WeaponType> _weaponTypes;
        private Camera _camera;
        private WaitForSeconds _weaponSwitching;
        private int _weaponTypeIndex;
        private int _weaponIndex;
        private float _lastWeaponReload;
        private bool _canShoot = true;
        private WeaponType _selectedWeaponType;

        private void Awake()
        {
            _camera = Camera.main;
            _weaponTypes = _weapons.Select(s => s.Type).ToList();
            _selectedWeaponType = _weaponTypes[_weaponTypeIndex];
            SelectWeapon();
            _inputHandler.OnInput += Shoot;
        }

        private void OnDestroy()
        {
            _inputHandler.OnInput -= Shoot;
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

        private void Shoot(Vector3 inputPos)
        {
            var weapon = _selectedWeapons[_weaponIndex];
            
            if (!_canShoot || !weapon.CanShoot)
            {
                return;
            }
                
            var ray = _camera.ScreenPointToRay(inputPos);
            var endPoint = Physics.Raycast(ray, out RaycastHit hit, _mask)
                ? hit.point
                : _camera.transform.position + ray.direction * weapon.FiringRange;
                
            weapon.Shoot(endPoint);

            if (weapon.ReloadTime > 0)
            {
                if (_lastWeaponReload != weapon.ReloadTime)
                {
                    _lastWeaponReload = weapon.ReloadTime;
                    _weaponSwitching = new WaitForSeconds(_lastWeaponReload);
                }
                    
                StartCoroutine(SwitchWeapon());
            }
                    
            NextWeaponFromSelected(); 
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