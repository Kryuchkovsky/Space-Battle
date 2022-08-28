using Logic.Spaceships.Interfaces;

namespace Logic.Spaceships.Behaviors
{
    public class DisabledMovingBehavior : IMoveable
    {
        public void Move(Spaceship spaceship)
        {
            //Do nothing
        }
    }
}
