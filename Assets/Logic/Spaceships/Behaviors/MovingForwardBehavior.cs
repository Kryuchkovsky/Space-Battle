using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class MovingForwardBehavior : IMoveable
    {
        public void Move(Spaceship spaceship)
        {
            spaceship.transform.position += spaceship.transform.forward * spaceship.MovementSpeed * Time.deltaTime;
        }
    }
}