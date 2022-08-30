using Logic.Data;
using Logic.Spaceships;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Services
{
    public abstract class BaseBattleBuilder : MonoBehaviour
    {
        [SerializeField] protected LevelData _data;
        
        public InputHandler InputHandler { get; set; }
        
        public abstract Spaceship CreatePlayer();
        public abstract void StartBattle();
    }
}