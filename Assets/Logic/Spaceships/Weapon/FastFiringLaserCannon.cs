using System.Collections;
using Logic.Data;
using Logic.Patterns;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class FastFiringLaserCannon : BaseWeapon
    {
        [SerializeField] private BlasterCharge _blasterCharge;

        private ObjectPool<BlasterCharge> _blasterChargePool;
        private WaitForSeconds _reload;
        private bool _isDestroyed;

        private void Awake()
        {
            _blasterChargePool = new ObjectPool<BlasterCharge>(_blasterCharge, transform);
            _reload = new WaitForSeconds(_reloadTime);
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
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
            _effectManager.CreateEffectByType(EffectType.Sparks, charge.transform.position, Quaternion.identity);

            if (_isDestroyed)
            {
                Destroy(charge.gameObject);
            }
            else
            {
                _blasterChargePool.Return(charge);
            }

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