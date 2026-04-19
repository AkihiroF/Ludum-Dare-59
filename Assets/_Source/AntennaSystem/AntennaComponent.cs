using System.Collections.Generic;
using AntennaSystem.Data;
using InteractionSystem;
using UnityEngine;
using UnityEngine.Events;

namespace AntennaSystem
{
    public class AntennaComponent : MonoBehaviour, IInteraction, IAntenna
    {
        [SerializeField] private UnityEvent<AntennaState> onChangeState;
        public bool IsStarted;
        public bool DrawRadius;
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
                    UpdateRadius(false);
                    break;
                case AntennaState.Enabled:
                    EnableHighLight(false);
                    view.ChangeState();
                    UpdateRadius(false);
                    SignalConnector.SetCurrent(null, null);
                    break;
                case AntennaState.SearchConnection:
                    view.ChangeState();
                    EnableHighLight();
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
            Debug.Log($"{_currentState} - {isEnabled}");
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
            if(DrawRadius && view is not null)
                Gizmos.DrawSphere(view.transform.position, Settings.radius);
        }
    }
}