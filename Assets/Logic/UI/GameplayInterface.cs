using Logic.Spaceships.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI
{
    public class GameplayInterface : MonoBehaviour
    {
        [SerializeField] private AimHandler _aimHandler;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Button _nextWeaponButton;
        [SerializeField] private Button _previousWeaponButton;

        public AimHandler AimHandler => _aimHandler;
        public InputHandler InputHandler => _inputHandler;
        public Button NextWeaponButton => _nextWeaponButton;
        public Button PreviousWeaponButton => _previousWeaponButton;
    }
}