using System;
using UI;
using UnityEngine;

namespace AntennaSystem
{
    public class ModificationService : MonoBehaviour
    {
        [SerializeField] private ModificationView modificationView;
        public Action<int> OnCountModificatorChanged;
        private int _countsOfScale;
        private RadiusBoostModifier _currentModificator;

        public bool TryUseModification()
        {
            if (_countsOfScale == 0)
                return false;
            if(SignalConnector.CurrentAntenna is null)
                return  false;
            _countsOfScale--;
            modificationView.ChangeCount(_currentModificator.Multiplier, _countsOfScale);
            SignalConnector.CurrentAntenna.AddModifier(_currentModificator);
            OnCountModificatorChanged?.Invoke(_countsOfScale);
            return  true;
        }

        public void Clear()
        {
            _countsOfScale = 0;
            _currentModificator = null;
            modificationView.ChangeCount(0, 0);
        }

        public void ChangeCount(float scale, int count)
        {
            _countsOfScale = count;
            _currentModificator = new RadiusBoostModifier(scale);
            modificationView.ChangeCount(scale, count);
            OnCountModificatorChanged?.Invoke(_countsOfScale);
        }
        
    }
}