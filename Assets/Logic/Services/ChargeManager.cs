using Logic.Patterns;
using Logic.Spaceships.Weapon;
using UnityEngine;

namespace Logic.Services
{
    public class ChargeManager : MonoBehaviour
    {
        public static ChargeManager Instance = null;

        [SerializeField] private BlasterCharge _chargePrefab;

        private ObjectPool<BlasterCharge> _chargePool;

        private void Awake()
        {
            if (Instance == null) 
            {
                Instance = this;
            } 
            else if (Instance == this)
            {
                Destroy(gameObject);
            }

            _chargePool = new ObjectPool<BlasterCharge>(_chargePrefab, transform);
        }

        public BlasterCharge Create(Vector3 position, Quaternion rotation)
        {
            var charge = _chargePool.Take(position, rotation);
            charge.Callback += _chargePool.Return;

            return charge;
        }
    }
}