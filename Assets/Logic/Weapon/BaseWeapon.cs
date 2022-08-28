using UnityEngine;

namespace Logic.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected Transform _shotPoint;
        [SerializeField] [Min(0)] protected float _firingRange = 1000;
        [SerializeField] [Min(0)] protected float _reloadTime = 0.25f;
        [SerializeField] protected WeaponType _type;
        
        protected bool _canShoot;
        
        public float FiringRange => _firingRange;
        public float ReloadTime => _reloadTime;
        public bool CanShoot => _canShoot;
        public WeaponType Type => _type;
    
        public abstract void Shoot(Vector3 endPoint);
    }

    public enum WeaponType
    {
        FastFiringLaserCannon,
        RayGun
    }
}