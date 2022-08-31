using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.Spaceships.Services
{
    public class InputHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private LayerMask _mask;
        
        private Vector3 _point;
        private bool _hasInput;

        public InputData InputData { get; private set; }
        public float FiringRange { get; set; } = 10000;
        
        private void Update()
        {
            if (_hasInput)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                _point = Physics.Raycast(ray, out RaycastHit hit, _mask)
                    ? hit.point
                    : Camera.main.transform.position + ray.direction * FiringRange;
            }
            
            InputData = new InputData(_point, _hasInput);
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