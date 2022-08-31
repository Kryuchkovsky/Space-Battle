using Logic.Spaceships.Interfaces;
using Logic.Spaceships.Services;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class PlayerShootingBehavior : IShootable
    {
        private readonly InputHandler _inputHandler;
        
        public PlayerShootingBehavior(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }
        
        public void Shoot(Spaceship spaceship, BaseWeaponHolder weaponHolder)
        {
            if (_inputHandler.InputData.HasInput)
            {
                weaponHolder.Shoot(_inputHandler.InputData.Point);
            }
        }
    }
}