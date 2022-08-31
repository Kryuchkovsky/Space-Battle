using System.Collections;
using Logic.Data;
using Logic.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class FastFiringLaserCannon : BaseWeapon
    {
        private ChargeManager _chargeManager = ChargeManager.Instance;
        private WaitForSeconds _reload;

        private void Awake()
        {
            _reload = new WaitForSeconds(_reloadTime);
        }

        public override void Shoot(Vector3 endPoint)
        {
            if (IsReady)
            {
                var rotation = Quaternion.LookRotation(endPoint - _shotPoint.position);
                var charge = _chargeManager.Create(_shotPoint.position, rotation);
                charge.Init(DamageAgent, FiringRange, _damage);
                charge.Callback += x => _effectManager.CreateEffectByType(EffectType.Sparks, x.transform.position, x.transform.rotation);
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            IsReady = false;
            yield return _reload;
            IsReady = true;
        }
    }
}