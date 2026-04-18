using Utils;
using Zenject;

namespace Core.States
{
    public sealed class ExitState : AGameState
    {
        private readonly PlayerInputActions _playerInputActions;
        
        private readonly SceneLoader _sceneLoader;
        [Inject]
        public ExitState(PlayerInputActions playerInputActions, SceneLoader exitLoader)
        {
            _playerInputActions = playerInputActions;
            _sceneLoader = exitLoader;
        }

        public override void Enter()
        {
            _playerInputActions.Player.Disable();
            _sceneLoader.SimpleLoadScene();
        }
    }
}