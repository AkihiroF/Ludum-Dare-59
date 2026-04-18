using System;
using Camera;
using UnityEngine;
using UnityEngine.InputSystem;
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
            Subscribe();
        }

        private void Subscribe()
        {
            _playerInputActions.Player.Sprint.performed += OnSprint;
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            var isPressed = Math.Abs(obj.ReadValue<float>() - 1) < 1;
            _cameraMover.EnableSprint(isPressed);
        }

        private void InputMove()
        {
            var moveValue = _playerInputActions.Player.Moving.ReadValue<Vector2>();
            _cameraMover.UpdateMove(moveValue);
        }

        private void Update()
        {
            InputMove();
        }
        private void UnSubscribe()
        {
            _playerInputActions.Player.Sprint.performed -= OnSprint;
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }
    }
}