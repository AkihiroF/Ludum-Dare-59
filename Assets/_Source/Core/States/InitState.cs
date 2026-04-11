using Core.CoreEvents;
using Input;
using Utils;
using Zenject;

namespace Core.States
{
    public sealed class InitState : AGameState
    {
        [Inject]
        public InitState(InputHandler inputHandler, PlayerInputActions playerInputActions)
        {
            _inputHandler = inputHandler;
            _playerInputActions = playerInputActions;
        }

        private readonly InputHandler _inputHandler;
        private readonly PlayerInputActions _playerInputActions;

        public override void Enter()
        {
            _playerInputActions.Move.Look.performed += _inputHandler.InputLook;
            _playerInputActions.Move.Moving.performed += _inputHandler.InputMove;
            
            Signals.Get<OnInitFinished>().Dispatch();
        }
    }
}