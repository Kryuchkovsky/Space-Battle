using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class AimHandler : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] [Min(1)] private float _maxScale = 1.5f;
        [SerializeField] [Min(0)] private float _valueIncrement = 0.1f;
        [SerializeField] [Min(0)] private float _decreasingValuePerSecond = 0.5f;

        private float _scale = 1;

        private void Update()
        {
            _image.transform.position = Input.mousePosition;
            _scale = Mathf.Clamp(_scale - _decreasingValuePerSecond * Time.deltaTime, 1, _maxScale);
            _image.transform.localScale = Vector3.one * _scale;
        }

        public void IncreaseAim() => _scale = Mathf.Clamp(_scale + _valueIncrement, 1, _maxScale);
        public void SetStatus(bool status) => _image.gameObject.SetActive(status);
    }
}