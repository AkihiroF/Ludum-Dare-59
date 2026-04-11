using Core.CoreEvents;
using Core.States;
using UnityEngine;
using Utils;
using Zenject;

namespace Core
{
    public class GameStarter : MonoBehaviour
    {
        [Inject]
        private StateMachine _stateMachine;

        private void Awake()
        {
            Signals.Get<OnInitFinished>().AddListener(StartGame);
        }

        private void StartGame()
        {
            _stateMachine.SwitchGameState<GameState>();
            Debug.Log("GameStarted");
        }

        public void ExitGame()
        {
            _stateMachine.SwitchGameState<ExitState>();
        }

        private void OnDestroy()
        {
            Signals.Get<OnInitFinished>().RemoveListener(StartGame);
        }
    }
}