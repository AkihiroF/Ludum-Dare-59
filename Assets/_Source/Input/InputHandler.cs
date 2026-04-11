using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public class InputHandler
    {
        private readonly InputTester _tester;
        [Inject]
        public InputHandler(InputTester tester)
        {
            _tester = tester;
        }

        public void InputLook(InputAction.CallbackContext obj)
        {
            var lookValue = obj.ReadValue<Vector2>();
            _tester.UpdateLook(lookValue);
        }

        public void InputMove(InputAction.CallbackContext obj)
        {
            var moveValue = obj.ReadValue<Vector2>();
            _tester.UpdateMove(moveValue);
        }
    }
}