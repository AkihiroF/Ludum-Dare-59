using UnityEngine;

namespace InteractionSystem
{
    public class TestInteract : MonoBehaviour, IInteraction
    {
        public void OnLook(bool isEnabled = true)
        {
            transform.localScale = isEnabled ?  Vector3.one * 1.2f: Vector3.one;
        }

        public void Interact()
        {
            UnityEngine.Debug.Log("VAR");
        }
    }
}