using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.Weapon
{
    public class InputHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public event Action<Vector3> OnInput;
        
        private bool _hasInput;
        
        private void Update()
        {
            if (_hasInput)
            {
                OnInput?.Invoke(Input.mousePosition);
            }
        }

        public void OnPointerDown(PointerEventData eventData) => _hasInput = true;
        public void OnPointerUp(PointerEventData eventData) => _hasInput = false;
    }
}