using UnityEngine;

namespace Logic.Weapon
{
    public class RayGun : BaseWeapon
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] [Min(0)] private float _turnOffDelay = 0.25f;

        private float _delay;
        
        private void Update()
        {
            _delay = Mathf.Clamp(_delay - Time.deltaTime, 0, _turnOffDelay);
            _lineRenderer.enabled = _delay > 0;
        }

        public override void Shoot(Vector3 endPoint)
        {
            _delay = _turnOffDelay;
            _lineRenderer.SetPosition(0, _shotPoint.position);
            _lineRenderer.SetPosition(1,  endPoint);
        }
    }
}