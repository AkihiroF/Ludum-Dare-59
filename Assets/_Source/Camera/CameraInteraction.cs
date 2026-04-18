using Camera.Data;
using InteractionSystem;
using UnityEngine;

namespace Camera
{
    public class CameraInteraction : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera targetCamera;
        [SerializeField] private InteractionSettings settings;
        [SerializeField] private AnimationUpdateMode updateMode;

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
                    settings.distanceForInteract, settings.interactionLayer) is false)
            {
                _currentInteraction?.OnLook(false);
                _currentInteraction = null;
                return;
            }

            if (hit.collider.TryGetComponent(out IInteraction interaction) is false) 
                return;
            _currentInteraction = interaction;
            _currentInteraction.OnLook();
        }
    }
}