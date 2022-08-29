using UnityEngine;

namespace Logic.Spaceships.Services
{
    public abstract class BaseWeaponHolder : MonoBehaviour
    {
        public abstract void Shoot(Vector3 targetPosition);
    }
}