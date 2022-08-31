using System.Collections.Generic;
using System.Linq;
using Logic.Visual;
using UnityEngine;

namespace Logic.Data
{
    [CreateAssetMenu]
    public class EffectConfig : ScriptableObject
    {
        [SerializeField] private List<Effect> _effect;

        public Effect GetEffectByType(EffectType type) => _effect.First(x => x.Type == type);
    }
    
    public enum EffectType
    {
        Explosion,
        Melting,
        Sparks
    }
}