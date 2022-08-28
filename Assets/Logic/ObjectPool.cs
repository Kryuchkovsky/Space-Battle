using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private Stack<T> _objects;
        private T _prefab;
        private Transform _parent;

        public ObjectPool(T prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _objects = new Stack<T>();
        }

        public T Take(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            T obj = _objects.Count == 0 ? Object.Instantiate(_prefab, _parent) : _objects.Pop();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.gameObject.SetActive(true);

            return obj;
        }

        public void Return(T obj)
        {
            _objects.Push(obj);
            obj.gameObject.SetActive(false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
}