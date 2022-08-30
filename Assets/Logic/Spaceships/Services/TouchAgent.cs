using System;
using UnityEngine;

namespace Logic.Spaceships.Services
{
    public class TouchAgent : MonoBehaviour
    {
        public event Action OnTouch;
        
        private void OnMouseDown() => OnTouch?.Invoke();
    }
}