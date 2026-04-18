using System;
using System.Collections.Generic;
using AntennaSystem.Data;
using InteractionSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace AntennaSystem
{
    public class AntennaComponent : MonoBehaviour, IInteraction
    {
        [SerializeField] private UnityEvent<AntennaState> onChangeState;
        public bool IsStarted;
        [field: SerializeField] public AntennaSettings Settings { get; private set; }
        [SerializeField] private RadiusView view;
        [SerializeField] private AntennaHighLightView antennaHighLightView;
        [field: SerializeField] public uint ID { get; private set; }
        [SerializeField]private List<AntennaComponent>_antennaComponentsInRange = new();

        private AntennaState _currentState;
        public bool IsCurrentHasSignal { get; private set; }

#if UNITY_EDITOR
        [ContextMenu("Generate ID")]
        public void GenerateID()
        {
            ID = Guid.NewGuid().ToUint();
            EditorUtility.SetDirty(this);
        }
#endif

        private void Awake()
        {
            view.Init(Settings);
            SetState(IsStarted ? AntennaState.Enabled : AntennaState.Disabled);
            //view.ChangeState(false); //change state of view radius of antenna
        }

        public void OnLook(bool isEnabled = true)
        {
            if(_currentState is AntennaState.Disabled)
                antennaHighLightView.ChangeScale(isEnabled);
        }

        public void Interact()
        {
            switch (_currentState)
            {
                case AntennaState.Disabled:
                    SignalConnector.TryConnect(this);
                    break;
                case AntennaState.Enabled:
                    SetState(AntennaState.SearchConnection);
                    break;
                case AntennaState.SearchConnection:
                    SetState(AntennaState.Enabled);
                    break;
            }
        }
        private void SetState(AntennaState newState)
        {
            _currentState = newState;
            switch (_currentState)
            {
                case AntennaState.Disabled:
                    IsCurrentHasSignal = false;
                    view.ChangeState(false);
                    IsCurrentHasSignal = false;
                    EnableHighLight(false);
                    break;
                case AntennaState.Enabled:
                    view.ChangeState();
                    view.ChangeColor(false);
                    FindAntennasInRange();
                    foreach (var antenna in _antennaComponentsInRange)
                    {
                        antenna.EnableHighLight(false);
                        antenna.enabled = false;
                    }
                    SignalConnector.SetCurrent(null, null);
                    break;
                case AntennaState.SearchConnection:
                    view.ChangeColor();
                    view.ChangeState();
                    FindAntennasInRange();
                    foreach (var antenna in _antennaComponentsInRange)
                    {
                        antenna.enabled = true;
                        antenna.EnableHighLight();
                    }
                    SignalConnector.SetCurrent(this, _antennaComponentsInRange);
                    break;
            }
        }
        
        public virtual void ReceiveSignalFrom(AntennaComponent from)
        {
            if (from == null) return;
            from.SetState(AntennaState.Disabled);
            SetState(AntennaState.Enabled);
            IsCurrentHasSignal = true;
        }

        private void EnableHighLight(bool isEnabled = true)
        {
            antennaHighLightView.ChangeState(isEnabled);
            antennaHighLightView.enabled = isEnabled;
        }

        private void FindAntennasInRange()
        {
            _antennaComponentsInRange = new();
            var hits = Physics.SphereCastAll(view.transform.position, Settings.radius, Vector3.up, Settings.radius);
            if(hits is {Length: 0})
                return;
            foreach (var hit in hits)
            {
                if(hit.collider is not null)
                    if(hit.collider.TryGetComponent(out AntennaComponent antenna))
                        if(antenna != this)
                            // if(Physics.Raycast(view.transform.position, view.transform.position.GetDirection(hit.point), out var hitInfo))
                            //     if(hitInfo.collider == hit.collider)
                                    _antennaComponentsInRange.Add(antenna);
            }
        }

        private void OnDrawGizmosSelected()
        {
            //Gizmos.DrawSphere(transform.position, Settings.radius);
        }
    }
}