using UnityEngine;

namespace Core.States
{
    public class PauseState : AGameState
    {
        public override void Enter()
        {
            Time.timeScale = .01f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}