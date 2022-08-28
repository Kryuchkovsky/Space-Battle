using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.Spaceships.Services
{
    public class InputHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public event Action<Ray> OnInput;
        
        private bool _hasInput;
        
        private void Update()
        {
            if (_hasInput)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                OnInput?.Invoke(ray);
            }
        }

        public void SetStatus(bool status) => gameObject.SetActive(status);
        public void OnPointerDown(PointerEventData eventData) => _hasInput = true;
        public void OnPointerUp(PointerEventData eventData) => _hasInput = false;
    }
}