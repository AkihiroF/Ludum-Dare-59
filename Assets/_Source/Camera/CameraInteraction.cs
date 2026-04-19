using Camera.Data;
using InteractionSystem;
using UI;
using UnityEngine;

namespace Camera
{
    public class CameraInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera targetCamera;
        [SerializeField] private InteractionSettings settings;
        [SerializeField] private AnimationUpdateMode updateMode;
        [SerializeField] private WindowStateSwitcher interactWindow;
        public Vector3 CurrentPoint { get; private set; }

        private IInteraction _currentInteraction;
        private void Update()
        {
            if (updateMode is not AnimationUpdateMode.Normal)
                return;
            CheckInteraction();
        }

        private void FixedUpdate()
        {
            if(updateMode is not AnimationUpdateMode.Fixed)
                return;
            CheckInteraction();
        }

        public void Interact()
            => _currentInteraction?.Interact();
        
        private void CheckInteraction()
        {
            var camTransform = targetCamera.transform;
            if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit,
                    settings.distanceForInteract) is false)
            {
                interactWindow.ChangeState(false);
                _currentInteraction?.OnLook(false);
                _currentInteraction = null;
                
                CurrentPoint = camTransform.position + camTransform.forward * settings.distanceForInteract;
                return;
            }
            CurrentPoint = hit.point;
            if (hit.collider.TryGetComponent(out IInteraction interaction) is false) 
                return;
            _currentInteraction = interaction;
            _currentInteraction.OnLook();
            interactWindow.ChangeState();
        }
    }
}