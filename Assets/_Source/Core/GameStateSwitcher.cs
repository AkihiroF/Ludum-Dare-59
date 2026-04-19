using Core.CoreEvents;
using Core.States;
using DG.Tweening;
using UI;
using UnityEngine;
using Utils;
using Zenject;

namespace Core
{
    public class GameStateSwitcher : MonoBehaviour
    {
        [SerializeField] private WindowStateSwitcher pauseWindow;
        [SerializeField] private WindowStateSwitcher finishWindow;
        [SerializeField] private WindowStateSwitcher failedWindow;
        [Inject]
        private StateMachine _stateMachine;

        private void Awake()
        {
            Signals.Get<OnInitFinished>().AddListener(StartGame);
        }

        public void StartGame()
        {
            pauseWindow.ChangeState(false);
            _stateMachine.SwitchGameState<GameState>();
        }

        public void Pause()
        {
            pauseWindow.ChangeState();
            _stateMachine.SwitchGameState<PauseState>();
        }

        public void ExitGame()
        {
            pauseWindow.ChangeState(false);
            _stateMachine.SwitchGameState<ExitState>();
        }

        private void OnDestroy()
        {
            Signals.Get<OnInitFinished>().RemoveListener(StartGame);
            DOTween.KillAll();
        }

        public void GameFinished(bool isComplete)
        {
            var targetWindow = isComplete ? finishWindow : failedWindow;
            targetWindow.ChangeState();
            _stateMachine.SwitchGameState<PauseState>();
        }
    }
}