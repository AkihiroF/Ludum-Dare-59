using System;
using LevelSystem;
using UI;
using UnityEngine;

namespace AntennaSystem
{
    public class ModificationService : MonoBehaviour
    {
        [SerializeField] private ModificationView modificationView;
        public Action<int> OnCountModificatorChanged;
        public int CountsOfScale { get; private set; }
        private RadiusBoostModifier _currentModificator;

        public bool TryUseModification()
        {
            if (CountsOfScale == 0)
                return false;
            if(SignalConnector.CurrentAntenna is null)
                return  false;
            CountsOfScale--;
            modificationView.ChangeCount(_currentModificator.Multiplier, CountsOfScale);
            SignalConnector.CurrentAntenna.AddModifier(_currentModificator);
            OnCountModificatorChanged?.Invoke(CountsOfScale);
            return  true;
        }

        public void Clear()
        {
            CountsOfScale = 0;
            _currentModificator = null;
            modificationView.ChangeCount(0, 0);
        }

        public void ChangeCount(float scale, int count)
        {
            CountsOfScale = count;
            _currentModificator = new RadiusBoostModifier(scale);
            modificationView.ChangeCount(scale, count);
            OnCountModificatorChanged?.Invoke(CountsOfScale);
        }
        
    }
}