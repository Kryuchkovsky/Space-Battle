using System;
using Logic.Data;
using UnityEngine;

namespace Logic.Visual
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private EffectType _type;
        
        public Action<Effect> Callback { get; set; }
        public EffectType Type => _type;
        
        private void Awake()
        {
            _particleSystem.Play();
            var main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Callback?.Invoke(this);
            Callback = null;
        }
    }
}