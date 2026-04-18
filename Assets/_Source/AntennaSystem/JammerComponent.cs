using System;
using System.Collections.Generic;
using AntennaSystem.Data;
using UnityEngine;

namespace AntennaSystem
{
    public class JammerComponent : MonoBehaviour, IAntenna
    {
        [field: SerializeField] public float Radius { get; private set; }
        [SerializeField] private float scaleJammer;
        [SerializeField] private RadiusView view;

        private AntennaComponent[] _antennaComponents;
        private RadiusBoostModifier _boostModifier;

        private void Awake()
        {
            view.Init(this);
        }

        private void OnEnable()
        {
            var hits = Physics.SphereCastAll(transform.position, Radius, Vector3.up, Radius);
            if (hits is { Length: 0 })
                return;
            var antennaComponentsInRange = new List<AntennaComponent>();
            foreach (var hit in hits)
            {
                if (hit.collider is not null)
                    if (hit.collider.TryGetComponent(out AntennaComponent antenna))
                        antennaComponentsInRange.Add(antenna);
            }
            _antennaComponents = antennaComponentsInRange.ToArray();
            _boostModifier = new RadiusBoostModifier(scaleJammer);
            foreach (var antenna in _antennaComponents)
            {
                antenna.AddModifier(_boostModifier);
            }
            view.ChangeState();
        }

        public float GetCurrentRadius()
            => Radius;
    }
}