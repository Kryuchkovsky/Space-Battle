using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class AimHandler : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void SetStatus(bool status) => _image.gameObject.SetActive(status);

        private void Update()
        {
            _image.transform.position = Input.mousePosition;
        }
    }
}