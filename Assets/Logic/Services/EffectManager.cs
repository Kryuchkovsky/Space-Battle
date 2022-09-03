using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Data;
using Logic.Patterns;
using Logic.Visual;
using UnityEngine;

namespace Logic.Services
{
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager Instance;

        [SerializeField] private EffectConfig _config;

        private List<ObjectPool<Effect>> _effectPools;

        private void Awake()
        {
            if (Instance == null) 
            {
                Instance = this;
            } 
            else if (Instance == this)
            {
                Destroy(gameObject);
            }

            _effectPools = new List<ObjectPool<Effect>>();

            foreach (EffectType type in Enum.GetValues(typeof(EffectType)))
            {
                var prefab = _config.GetEffectByType(type);
                _effectPools.Add(new ObjectPool<Effect>(prefab, transform));
            }
        }

        public Effect CreateEffectByType(EffectType type, Vector3 position, Quaternion rotation)
        {
            var effect = _effectPools.First(x => x.Prefab.Type == type).Take(position, rotation);
            effect.Callback += _effectPools.First(x => x.Prefab.Type == effect.Type).Return;

            return effect;
        }
    }
}
