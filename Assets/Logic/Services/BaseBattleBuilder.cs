using Logic.Spaceships;
using UnityEngine;

namespace Logic.Services
{
    public abstract class BaseBattleBuilder : MonoBehaviour
    {
        public abstract void Init();
        public abstract Spaceship CreatePlayer();
        public abstract void StartBattle();
    }
}