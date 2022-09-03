using System.Collections;
using Logic.Data;
using Logic.Services;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public class FastFiringLaserCannon : BaseWeapon
    {
        private ChargeManager _chargeManager = ChargeManager.Instance;
        private WaitForSeconds _reload;

        public override void Init(DamageAgent agent, WeaponData data)
        {
            base.Init(agent, data);
            _reload = new WaitForSeconds(_data.ReloadTime);
        }

        public override void Shoot(Vector3 endPoint)
        {
            if (IsReady)
            {
                var rotation = Quaternion.LookRotation(endPoint - _shotPoint.position);
                var charge = _chargeManager.Create(_shotPoint.position, rotation);
                charge.Init(_agent, _data);
                charge.Callback += x => _effectManager.CreateEffectByType(EffectType.Sparks, x.transform.position, x.transform.rotation);
                _audioSource.PlayOneShot(_data.AudioClip);
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