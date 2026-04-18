using System;
using System.Linq;
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
        } 
        #endregion
        [SerializeField] private LevelSettings[] levels;
        [SerializeField] private CameraMover cameraMover;
        [SerializeField] private GameStateSwitcher gameStateSwitcher;
        private int _currentIndex = 0;

        private void Start()
        {
            cameraMover.RebuildArea(levels[_currentIndex].areaForMoving.Select(area => area.position).ToArray());
            levels[_currentIndex].rootOfLevel.SetActive(true);
        }

        public void NextLevel()
        {
            levels[_currentIndex].rootOfLevel.SetActive(false);
            _currentIndex++;
            if (_currentIndex >= levels.Length)
            {
                gameStateSwitcher.GameFinished();
                return;
            }
            levels[_currentIndex].rootOfLevel.SetActive(true);
            cameraMover.RebuildArea(levels[_currentIndex].areaForMoving.Select(area => area.position).ToArray());
        }
    }
}