using Logic.Spaceships.Behaviors;
using UnityEngine;

namespace Logic.Spaceships
{
    public class VulnerableArmedPursuingSpaceship: Spaceship
    {
        private int _weaponHolderIndex;

        protected void Update()
        {
            base.Update();
            
            if (Target && transform.forward == (Target.position - transform.position).normalized)
            {
                var ray = new Ray(transform.position, Target.position - transform.position);
                _shootable.Shoot( _weaponHolders[_weaponHolderIndex], ray);
            }
        }

        public override void InitBehaviors()
        {
            _damageable = new VulnerableState(_durabilityPoints);
            _moveable = new PursuingBehavior();
            _shootable = new BotShootingBehavior(this);
            Init();
        }

        public void NextWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == _weaponHolders.Count - 1 ? 0 : _weaponHolderIndex + 1;
        public void PreviousWeapon() =>  _weaponHolderIndex = _weaponHolderIndex == 0 ? _weaponHolders.Count - 1 : _weaponHolderIndex - 1;
        private void Shoot(Ray ray) => _shootable.Shoot(_weaponHolders[_weaponHolderIndex], ray);
    }
}