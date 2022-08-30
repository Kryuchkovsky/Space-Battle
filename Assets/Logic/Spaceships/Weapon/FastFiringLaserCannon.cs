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
            _blasterChargePool = new ObjectPool<BlasterCharge>(_blasterCharge, transform);
            _effectHitPool = new ObjectPool<Effect>(_hitEffectPrefab, transform);
            _reload = new WaitForSeconds(_reloadTime);
        }

        public override void Shoot(Vector3 endPoint)
        {
            if (IsReady)
            {
                var rotation = Quaternion.LookRotation(endPoint - _shotPoint.position);
                var charge = _blasterChargePool.Take(_shotPoint.position, rotation);
                charge.transform.parent = null;
                charge.Init(DamageAgent, FiringRange, _damage);
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
            IsReady = false;
            yield return _reload;
            IsReady = true;
        }
    }
}