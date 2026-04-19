using System;
using System.Linq;
using AntennaSystem;
using Camera;
using Core;
using UnityEngine;

namespace LevelSystem
{
    public class LevelSwitcher : MonoBehaviour
    {
        #region Internal
        [Serializable]
        private struct LevelSettings
        {
            public GameObject rootOfLevel;
            public Transform[] areaForMoving;
            public CountModificationData  countModifier;
        } 
        [Serializable]
        private struct CountModificationData
        {
            public float targetScale;
            public int targetCount;
        }
        #endregion
        [SerializeField] private LevelSettings[] levels;
        [SerializeField] private ModificationService modificationService;
        [SerializeField] private CameraMover cameraMover;
        [SerializeField] private GameStateSwitcher gameStateSwitcher;
        private int _currentIndex = 0;

        private void Start()
        {
            ChangeLevel();
        }

        public void NextLevel()
        {
            levels[_currentIndex].rootOfLevel.SetActive(false);
            _currentIndex++;
            if (_currentIndex >= levels.Length)
            {
                gameStateSwitcher.GameFinished(true);
                return;
            }

            ChangeLevel();
        }

        private void ChangeLevel()
        {
            levels[_currentIndex].rootOfLevel.SetActive(true);
            cameraMover.RebuildArea(levels[_currentIndex].areaForMoving.Select(area => area.position).ToArray());
            if(levels[_currentIndex].countModifier.targetCount > 0)
                UpdateModificators();
            else
                modificationService.Clear();
        }

        private void UpdateModificators()
        {
            var targetValues = levels[_currentIndex].countModifier;
            modificationService.ChangeCount(targetValues.targetScale, targetValues.targetCount);
        }
    }
}