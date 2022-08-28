using Logic.Spaceships.Interfaces;
using UnityEngine;

namespace Logic.Spaceships.Behaviors
{
    public class MovingForwardBehavior : IMoveable
    {
        private readonly float _speed;
        
        public MovingForwardBehavior(float speed)
        {
            _speed = speed;
        }
        
        public void Move(Spaceship spaceship)
        {
            spaceship.transform.position += spaceship.transform.forward * _speed * Time.deltaTime;
        }
    }
}