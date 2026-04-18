using UnityEngine;
using Zenject;

namespace Core.States
{
    public sealed class GameState : AGameState
    {
        private readonly PlayerInputActions _playerInputActions;

        [Inject]
        public GameState(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        public override void Enter()
        {
            _playerInputActions.Player.Enable();
            _playerInputActions.Interface.Enable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void Exit()
        {
            _playerInputActions.Player.Disable();
            _playerInputActions.Interface.Disable();
        }
    }
}