using System;
using Camera.Data;
using Unity.Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private CameraSettings settings;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        private Func<Vector3> _movementFunc;
        private float _speedMovement;
        private Vector3 _moveDirection;
        public void Awake()
        {
            settings.area.Init();
            _speedMovement = settings.speedMovement;
            _movementFunc = () => settings.area.GetPositionInArea(cinemachineCamera.transform.position + _moveDirection * _speedMovement);
        }


        public void RebuildArea(Vector3[] newPoints)
        {
            settings.area.Rebuild(newPoints);
            ResetCameraPosition(settings.area.CenterOfArea);
        }

        public void ResetCameraPosition(Vector3 targetPosition)
        {
            targetPosition.y = cinemachineCamera.transform.position.y;
            cinemachineCamera.transform.position = targetPosition;
            var targetDir = cinemachineCamera.transform.position - targetPosition;
            cinemachineCamera.ForceCameraPosition(targetPosition, Quaternion.LookRotation(targetDir));
        }

        private void OnDrawGizmos()
        {
            if(Application.isPlaying is false)
                return;
            if (settings.area is null) return;

            var bounds = settings.area.GetBounds();
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }

        public void UpdateLook(Vector2 lookValue)
        {
            
        }

        public void UpdateMove(Vector2 moveValue)
        {
            Transform cam = cinemachineCamera.transform;

            Vector3 forward = cam.forward;

            Vector3 right = cam.right;

            forward.y = 0f;

            right.y = 0f;

            forward.Normalize();

            right.Normalize();

            _moveDirection = forward * moveValue.y + right * moveValue.x;

            if (_moveDirection.sqrMagnitude > 1f)

                _moveDirection.Normalize();
        }

        private void Update()
        {
            cinemachineCamera.transform.position = Vector3.Lerp(cinemachineCamera.transform.position, _movementFunc.Invoke(), Time.deltaTime * settings.movementSmooth);
        }

        public void EnableSprint(bool isEnabled)
        {
            _speedMovement = isEnabled ? settings.speedMovement* settings.sprintMultiplier : settings.speedMovement;
        }
    }
}