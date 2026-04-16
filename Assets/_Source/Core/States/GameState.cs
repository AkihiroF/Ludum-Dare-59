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
        }
    }
}