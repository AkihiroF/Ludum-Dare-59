using Core.CoreEvents;
using Input;
using Utils;
using Zenject;

namespace Core.States
{
    public sealed class InitState : AGameState
    {
        [Inject]
        public InitState(PlayerInputActions playerInputActions)
        {
            _playerInputActions = playerInputActions;
        }

        private readonly InputHandler _inputHandler;
        private readonly PlayerInputActions _playerInputActions;

        public override void Enter()
        {
            Signals.Get<OnInitFinished>().Dispatch();
        }
    }
}