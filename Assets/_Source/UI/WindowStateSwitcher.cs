using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WindowStateSwitcher : MonoBehaviour
    {
        [SerializeField] private bool defaultEnabled;
        [SerializeField] protected float timeSwitching;
        protected Tween Tween;
        protected CanvasGroup TargetPanel;
        private bool _isCurrentEnabled;
        
        protected virtual void Awake()
        {
            TargetPanel = GetComponent<CanvasGroup>();
            
            TargetPanel.alpha = defaultEnabled ? 1 : 0;
            ChangeInteractive(defaultEnabled);
        }

        public void ChangeState(bool isEnabled = true)
        {
            if(TargetPanel is null)
                return;
            if(_isCurrentEnabled == isEnabled)
                return;
            Tween?.Kill();
            BuildAnimation(isEnabled);
            Tween.Play();
        }

        protected virtual void BuildAnimation(bool isEnabled = true)
        {
            //var seq = DOTween.Sequence();
            Tween = TargetPanel.DOFade(isEnabled ? 1f : 0f, timeSwitching).OnComplete(() => ChangeInteractive(isEnabled));
            Tween.SetUpdate(true);
        }

        protected void ChangeInteractive(bool isEnabled = true)
        {
            _isCurrentEnabled = isEnabled;
            TargetPanel.interactable = isEnabled;
            TargetPanel.blocksRaycasts = isEnabled;
        }
    }
}