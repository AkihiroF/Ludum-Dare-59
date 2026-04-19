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
    public class AntennaComponent : MonoBehaviour, IInteraction, IAntenna
    {
        [SerializeField] private UnityEvent<AntennaState> onChangeState;
        public bool IsStarted;
        [field: SerializeField] public AntennaSettings Settings { get; private set; }
        [SerializeField] private RadiusView view;
        [SerializeField] private AntennaHighLightView antennaHighLightView;
        public bool HasVariantsForConnect => _antennaComponentsInRange.Count > 0;
        private List<AntennaComponent>_antennaComponentsInRange = new();
        private List<IAntennaModifier> _modifiers = new();
        

        private AntennaState _currentState;
        public bool IsCurrentHasSignal { get; private set; }

        private void Awake()
        {
            view.Init(this);
            SetState(IsStarted ? AntennaState.Enabled : AntennaState.Disabled);
            //view.ChangeState(false); //change state of view radius of antenna
        }
        
        public void AddModifier(IAntennaModifier modifier)
        {
            _modifiers.Add(modifier);
            view.ChangeState(_currentState is not AntennaState.Disabled, true);
            UpdateRadius(_currentState is AntennaState.SearchConnection);
        }

        public void RemoveModifier(IAntennaModifier modifier)
        {
            _modifiers.Remove(modifier);
            view.ChangeState(_currentState is not AntennaState.Disabled, true);
            UpdateRadius(_currentState is not AntennaState.Disabled);
        }

        public void OnLook(bool isEnabled = true)
        {
            if(_currentState is AntennaState.Disabled)
                antennaHighLightView.ChangeScale(isEnabled);
        }
        public float GetCurrentRadius()
        {
            float value = Settings.radius;
            foreach (var mod in _modifiers)
            {
                value = mod.Modify(value);
            }
            return value;
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
                    UpdateRadius(false);
                    SignalConnector.SetCurrent(null, null);
                    break;
                case AntennaState.SearchConnection:
                    view.ChangeState();
                    UpdateRadius(true);
                    SignalConnector.SetCurrent(this, _antennaComponentsInRange);
                    break;
            }
        }

        private void UpdateRadius(bool isSearch)
        {
            view.ChangeColor(isSearch);
            FindAntennasInRange();
            SignalConnector.SetCurrent(this, _antennaComponentsInRange);
            foreach (var antenna in _antennaComponentsInRange)
            {
                antenna.enabled = isSearch;
                antenna.EnableHighLight(isSearch);
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
            var hits = Physics.SphereCastAll(view.transform.position, GetCurrentRadius(), Vector3.up, GetCurrentRadius());
            if(hits is {Length: 0})
                return;
            foreach (var hit in hits)
            {
                if(hit.collider is not null)
                    if(hit.collider.TryGetComponent(out AntennaComponent antenna))
                        if(antenna != this) 
                            _antennaComponentsInRange.Add(antenna);
            }
        }

        private void OnDrawGizmosSelected()
        {
            //Gizmos.DrawSphere(transform.position, Settings.radius);
        }
    }
}