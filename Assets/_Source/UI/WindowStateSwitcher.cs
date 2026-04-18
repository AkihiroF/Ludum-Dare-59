using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WindowStateSwitcher : MonoBehaviour
    {
        [SerializeField] private bool defaultEnabled;
        [SerializeField] private float timeSwitching;
        private CanvasGroup _targetPanel;
        
        private void Awake()
        {
            _targetPanel = GetComponent<CanvasGroup>();
            
            _targetPanel.alpha = defaultEnabled ? 1 : 0;
            ChangeInteractive(defaultEnabled);
        }

        public void ChangeState(bool isEnabled = true)
        {
            var seq = DOTween.Sequence();
            seq.SetUpdate(true);
            seq.Append(_targetPanel.DOFade(isEnabled ? 1f : 0f, timeSwitching).OnComplete(() => ChangeInteractive(isEnabled)));
            seq.Play();
        }

        private void ChangeInteractive(bool isEnabled = true)
        {
            _targetPanel.interactable = isEnabled;
            _targetPanel.blocksRaycasts = isEnabled;
        }
    }
}