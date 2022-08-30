using System.Collections;
using Logic.Patterns;
using Logic.Visual;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class FastFiringLaserCannon : BaseWeapon
    {
        [SerializeField] private BlasterCharge _blasterCharge;

        private ObjectPool<BlasterCharge> _blasterChargePool;
        private ObjectPool<Effect> _effectHitPool;
        private WaitForSeconds _reload;

        private void Awake()
        {
            _blasterChargePool = new ObjectPool<BlasterCharge>(_blasterCharge, null);
            _effectHitPool = new ObjectPool<Effect>(_hitEffectPrefab, null);
            _reload = new WaitForSeconds(_reloadTime);
        }

        public override void Shoot(Vector3 endPoint)
        {
            if (_canShoot)
            {
                var rotation = Quaternion.LookRotation(endPoint - _shotPoint.position);
                var charge = _blasterChargePool.Take(_shotPoint.position, rotation);
                charge.Damage = _damage;
                charge.RangeOfFlight = _firingRange;
                charge.OnDestruction += ReturnCharge;
                StartCoroutine(Reload());
            }
        }

        private void ReturnCharge(BlasterCharge charge)
        {
            var effect = _effectHitPool.Take(charge.transform.position);
            effect.Callback += () => _effectHitPool.Return(effect);
            _blasterChargePool.Return(charge);
            charge.OnDestruction -= ReturnCharge;
        }

        private IEnumerator Reload()
        {
            _canShoot = false;
            yield return _reload;
            _canShoot = true;
        }
    }
}