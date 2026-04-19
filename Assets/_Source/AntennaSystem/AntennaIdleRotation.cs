using System;

namespace AntennaSystem
{
    using UnityEngine;
    using DG.Tweening;

    public class AntennaIdleRotation : MonoBehaviour
    {
        [SerializeField] private float duration = 3f;
        private Tween _rotateTween;

        private void OnEnable()
        {
            SetActive(true);
        }

        private void OnDisable()
        {
            SetActive(false);
        }

        public void SetActive(bool state)
        {
            _rotateTween?.Kill();
            if (!state) return;
            _rotateTween = transform
                .DORotate(new Vector3(0, 360, 0), duration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
    }
}