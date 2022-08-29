using System.Collections.Generic;
using UnityEngine;

namespace Logic.Patterns
{
    public class ObjectPool<T> where T : Component
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
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;

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