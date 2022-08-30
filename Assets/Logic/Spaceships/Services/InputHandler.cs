using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.Spaceships.Services
{
    public class InputHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private Vector3 Point;
        private bool _hasInput;

        public InputData InputData { get; private set; }
        public float FiringRange { get; set; } = 10000;
        
        private void Update()
        {
            if (_hasInput)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Point = Physics.Raycast(ray, out RaycastHit hit)
                    ? hit.point
                    : Camera.main.transform.position + ray.direction * FiringRange;
            }
            
            InputData = new InputData(Point, _hasInput);
        }
        
        public void OnPointerDown(PointerEventData eventData) => _hasInput = true;
        public void OnPointerUp(PointerEventData eventData) => _hasInput = false;
    }

    public struct InputData
    {
        public Vector3 Point;
        public bool HasInput;

        public InputData(Vector3 point, bool hasInput)
        {
            Point = point;
            HasInput = hasInput;
        }
    }
}