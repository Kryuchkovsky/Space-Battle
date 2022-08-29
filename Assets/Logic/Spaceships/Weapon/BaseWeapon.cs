using Logic.Visual;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform _shotPoint;
        [SerializeField] protected Effect _hitEffectPrefab;
        [SerializeField] [Min(0)] protected float _damage = 50;
        [SerializeField] [Min(0)] protected float _firingRange = 1000;
        [SerializeField] [Min(0)] protected float _reloadTime = 0.25f;

        protected bool _canShoot = true;
        
        public float ReloadTime => _reloadTime;
        public bool CanShoot => _canShoot;

        public abstract void Shoot(Vector3 endPoint);
    }
}