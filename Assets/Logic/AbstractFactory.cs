using UnityEngine;

namespace Logic
{
    public abstract class AbstractFactory<T> where T : MonoBehaviour
    {
        public abstract T Create();
    }
}