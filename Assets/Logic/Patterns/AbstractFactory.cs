using UnityEngine;

namespace Logic.Patterns
{
    public abstract class AbstractFactory<T> where T : MonoBehaviour
    {
        public abstract T Create(Vector3 position, Quaternion rotation);
    }
}