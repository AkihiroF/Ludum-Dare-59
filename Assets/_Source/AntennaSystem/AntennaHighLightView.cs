using Camera;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace AntennaSystem
{
    public class AntennaHighLightView : MonoBehaviour
    {
        [SerializeField] private Transform[] wayForMovement;
        [SerializeField] private Transform rootOfAntenna;
        [SerializeField] private CameraInteraction cameraInteraction;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float speedRotation = 1.5f;
        [SerializeField] private Ease ease = Ease.InOutSine;
        private Tween _moveTween;
        
        private bool _isEnableLook = false;

        private void Awake()
        {
            cameraInteraction ??= FindAnyObjectByType<CameraInteraction>();
            rootOfAntenna ??= transform;
        }

        public void ChangeState(bool isEnabled)
        {
            if(isEnabled == _isEnableLook)
                return;
            _isEnableLook = isEnabled;
            if (wayForMovement == null || wayForMovement.Length == 0)
                return;
            _moveTween?.Kill();
            var path = isEnabled ? GetPathForward() : GetPathBackward();
            _moveTween = transform
                .DOPath(path, duration, PathType.CatmullRom)
                .SetEase(ease);
        }

        private void Update()
        {
            if (_isEnableLook is false)
                return;
            if(cameraInteraction is null)
                return;
            var target = cameraInteraction.CurrentPoint;
            var direction = rootOfAntenna.position.GetDirection(target, false);
            direction.y = 0;
            if (direction.sqrMagnitude < 0.001f)
                return;
            float targetY = Quaternion.LookRotation(direction).eulerAngles.y;
            float newY = Mathf.LerpAngle(
                rootOfAntenna.eulerAngles.y,
                targetY,
                Time.deltaTime * speedRotation
            );
            rootOfAntenna.rotation = Quaternion.Euler(
                rootOfAntenna.eulerAngles.x,
                newY,
                rootOfAntenna.eulerAngles.z
            );
        }

        public void ChangeScale(bool isBigger = true)
        {
        }
        private Vector3[] GetPathForward()
        {
            Vector3[] path = new Vector3[wayForMovement.Length];
            for (int i = 0; i < wayForMovement.Length; i++)
            {
                path[i] = wayForMovement[i].position;
            }
            return path;
        }
        private Vector3[] GetPathBackward()
        {
            int len = wayForMovement.Length;
            Vector3[] path = new Vector3[len];
            for (int i = 0; i < len; i++)
            {
                path[i] = wayForMovement[len - 1 - i].position;
            }
            return path;
        }
    }
}