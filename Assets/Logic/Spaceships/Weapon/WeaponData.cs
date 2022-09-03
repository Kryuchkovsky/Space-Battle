using System;
using UnityEngine;

namespace Logic.Spaceships.Weapon
{
    [Serializable]
    public class WeaponData
    {
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private Color _color = Color.red;
        [SerializeField] private float _damage = 100;
        [SerializeField] private float _firingRange = 10000;
        [SerializeField] private float _reloadTime = 0.25f;
        [SerializeField] private float _chargeSpeed = 1500;

        public AudioClip AudioClip => _audioClip;
        public Color Color => _color;
        public float Damage => _damage;
        public float FiringRange => _firingRange;
        public float ReloadTime => _reloadTime;
        public float ChargeSpeed => _chargeSpeed;

        public WeaponData(AudioClip audioClip, Color color, float firingRange, float damage, float chargeSpeed, float reloadTime)
        {
            _audioClip = audioClip;
            _color = color;
            _damage = damage;
            _firingRange = firingRange;
            _reloadTime = reloadTime;
            _chargeSpeed = chargeSpeed;
        }
    }
}