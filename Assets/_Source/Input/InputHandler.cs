using System;
using Camera;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private CameraMover cameraMover;
        [SerializeField] private CameraInteraction cameraInteraction;
        [SerializeField] private GameStateSwitcher gameStateSwitcher;
        private PlayerInputActions _playerInputActions;
        [Inject]
        public void Init(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
            Subscribe();
        }

        private void Subscribe()
        {
            _playerInputActions.Player.Sprint.performed += OnSprint;
            _playerInputActions.Player.Interact.performed += OnInteract;

            _playerInputActions.Interface.Paused.performed += TurnOnPause;
        }

        private void TurnOnPause(InputAction.CallbackContext obj)
        {
            gameStateSwitcher.Pause();
        }

        private void OnInteract(InputAction.CallbackContext obj)
        {
            cameraInteraction.Interact();
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            var isPressed = Math.Abs(obj.ReadValue<float>() - 1) < 1;
            cameraMover.EnableSprint(isPressed);
        }
        

        private void InputMove()
        {
            var moveValue = _playerInputActions.Player.Moving.ReadValue<Vector2>();
            cameraMover.UpdateMove(moveValue);
        }

        private void Update()
        {
            InputMove();
        }
        private void UnSubscribe()
        {
            _playerInputActions.Player.Sprint.performed -= OnSprint;
            _playerInputActions.Player.Interact.performed -= OnInteract;

            _playerInputActions.Interface.Paused.performed -= TurnOnPause;
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }
    }
}