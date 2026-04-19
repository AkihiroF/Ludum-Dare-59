using AntennaSystem;
using UnityEngine;

namespace Core
{
    public class FailedService : MonoBehaviour
    {
        [SerializeField] private GameStateSwitcher gameStateSwitcher;
        [SerializeField] private ModificationService modificationService;

        private AntennaComponent _currentAntenna;
        private int _currentCountModificator;
        private void Awake()
        {
            SignalConnector.CurrentAntennaChanged += SetNewAntenna;
            modificationService.OnCountModificatorChanged += UpdateCount;
        }

        private void UpdateCount(int newCount)
        {
            _currentCountModificator = newCount;
            Check();
        }

        private void OnDestroy()
        {
            SignalConnector.CurrentAntennaChanged -= SetNewAntenna;
            modificationService.OnCountModificatorChanged -= UpdateCount;
        }

        private void SetNewAntenna(AntennaComponent antenna)
        {
            _currentAntenna = antenna;
            Check();
        }
        

        private void Check()
        {
            if(_currentAntenna is not null 
               && _currentAntenna.IsCurrentHasSignal 
               && _currentAntenna.HasVariantsForConnect is false 
               && _currentCountModificator == 0)
            {
                Debug.Log(_currentAntenna.gameObject.name);
                Debug.Log(_currentAntenna.HasVariantsForConnect);
                Debug.Log(_currentCountModificator);
                gameStateSwitcher.GameFinished(false);
            }
        }
    }
}