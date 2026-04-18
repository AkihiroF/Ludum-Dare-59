using DG.Tweening;
using UnityEngine;

namespace AntennaSystem
{
    public class AntennaHighLightView : MonoBehaviour
    {
        [SerializeField] private Transform[] wayForMovement;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float maxScale = 1.5f;
        [SerializeField] private float durationScale = 0.5f;
        [SerializeField] private Ease ease = Ease.InOutSine;
        private Tween _moveTween;
        
        public void ChangeState(bool isEnabled)
        {
            if (wayForMovement == null || wayForMovement.Length == 0)
                return;
            _moveTween?.Kill();
            var path = isEnabled ? GetPathForward() : GetPathBackward();
            _moveTween = transform
                .DOPath(path, duration, PathType.CatmullRom)
                .SetEase(ease);
        }

        public void ChangeScale(bool isBigger = true)
        {
            if(enabled)
                transform.DOScale(isBigger ? maxScale : 1, durationScale);
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