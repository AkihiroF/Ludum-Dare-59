using Camera;
using UnityEngine;
using Zenject;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        private CameraMover _cameraMover;
        private PlayerInputActions _playerInputActions;
        [Inject]
        public void Init(CameraMover cameraMover, PlayerInputActions playerInputActions)
        {
            _cameraMover = cameraMover;
            _playerInputActions = playerInputActions;
        }
        
        public void InputMove()
        {
            var moveValue = _playerInputActions.Player.Moving.ReadValue<Vector2>();
            _cameraMover.UpdateMove(moveValue);
        }

        private void Update()
        {
            InputMove();
        }
    }
}