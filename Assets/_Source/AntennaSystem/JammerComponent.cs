using System.Collections.Generic;
using AntennaSystem.Data;
using DG.Tweening;
using UnityEngine;

namespace AntennaSystem
{
    public class JammerComponent : MonoBehaviour, IAntenna
    {
        public bool DrawRadius = true;

        [field: SerializeField] public Vector2 Radius { get; private set; } // x = min, y = max
        [SerializeField] private bool isUseRange;
        [SerializeField] private float timeLoop = 2f;
        [SerializeField] private float scaleJammer = 0.5f;
        [SerializeField] private RadiusView view;

        private HashSet<AntennaComponent> _currentAntennas = new();
        private RadiusBoostModifier _modifier;

        private float _currentRadius;
        private Tween _radiusTween;

        private void Awake()
        {
            view.Init(this);
            _modifier = new RadiusBoostModifier(scaleJammer);
        }

        private void OnEnable()
        {
            _currentRadius = Radius.y;
            if (isUseRange)
            {
                StartRadiusLoop();
            }
            view.ChangeState();
        }

        private void OnDisable()
        {
            _radiusTween?.Kill();
            foreach (var antenna in _currentAntennas)
            {
                antenna.RemoveModifier(_modifier);
            }

            _currentAntennas.Clear();
        }

        private void FixedUpdate()
        {
            UpdateAntennasInRange();
        }
    
        private void StartRadiusLoop()
        {
            _radiusTween?.Kill();

            _radiusTween = DOTween.To(
                    () => _currentRadius,
                    r => _currentRadius = r,
                    Radius.x,
                    timeLoop
                )
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
        private void UpdateAntennasInRange()
        {
            var hits = Physics.OverlapSphere(view.transform.position, _currentRadius);

            HashSet<AntennaComponent> newSet = new();

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out AntennaComponent antenna))
                {
                    newSet.Add(antenna);
                    if (!_currentAntennas.Contains(antenna))
                    {
                        antenna.AddModifier(_modifier);
                    }
                }
            }
            foreach (var antenna in _currentAntennas)
            {
                if (!newSet.Contains(antenna))
                {
                    antenna.RemoveModifier(_modifier);
                }
            }

            _currentAntennas = newSet;
            view.UpdateRadius();
        }

        public float GetCurrentRadius()
            => _currentRadius;

        private void OnDrawGizmosSelected()
        {
            if (DrawRadius)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, Application.isPlaying ? _currentRadius : Radius.y);
            }
        }
    }
}