using System.Collections;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class FastFiringLaserCannon : BaseWeapon
    {
        [SerializeField] private BlasterCharge _blasterCharge;

        private ObjectPool<BlasterCharge> _blasterChargePool;
        private ObjectPool<ParticleSystem> _effectHitPool;
        private WaitForSeconds _reload;

        private void Awake()
        {
            _blasterChargePool = new ObjectPool<BlasterCharge>(_blasterCharge, transform);
            _effectHitPool = new ObjectPool<ParticleSystem>(_hitEffectPrefab, transform);
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

        public void ReturnCharge(BlasterCharge charge)
        {
            Instantiate(_hitEffectPrefab, charge.transform.position, Quaternion.identity, transform);
            _blasterChargePool.Return(charge);
            charge.OnDestruction -= ReturnCharge;
        }

        public IEnumerator Reload()
        {
            _canShoot = false;
            yield return _reload;
            _canShoot = true;
        }
    }
}