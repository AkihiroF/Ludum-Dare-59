using Input;
using Utils;
using Zenject;

namespace Core.States
{
    public sealed class ExitState : AGameState
    {
        private readonly InputHandler _inputHandler;
        private readonly PlayerInputActions _playerInputActions;
        
        private readonly SceneLoader _sceneLoader;
        [Inject]
        public ExitState(InputHandler inputHandler, PlayerInputActions playerInputActions, SceneLoader exitLoader)
        {
            _inputHandler = inputHandler;
            _playerInputActions = playerInputActions;
            _sceneLoader = exitLoader;
        }

        public override void Enter()
        {
            _playerInputActions.Player.Rotate.performed -= _inputHandler.InputLook;
            _playerInputActions.Player.Moving.performed -= _inputHandler.InputMove;
            _playerInputActions.Player.Disable();
            _sceneLoader.SimpleLoadScene();
        }
    }
}