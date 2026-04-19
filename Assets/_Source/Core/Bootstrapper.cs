using Core.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine.SwitchGameState<InitState>();
        }
    }
}