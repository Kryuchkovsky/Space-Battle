using System;
using UnityEngine;

namespace Logic.Visual
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public ParticleSystem ParticleSystem => _particleSystem;
        public Action Callback { get; set; }
        
        private void Awake()
        {
            _particleSystem.Play();
            var main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Callback?.Invoke();
            Callback = null;
        }
    }
}