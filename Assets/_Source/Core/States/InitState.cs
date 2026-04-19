using Core.CoreEvents;
using Input;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.States
{
    public sealed class InitState : AGameState
    {
        [Inject]
        public InitState()
        {
        }

        public override void Enter()
        {
            Time.timeScale = 1;
            Signals.Get<OnInitFinished>().Dispatch();
        }
    }
}