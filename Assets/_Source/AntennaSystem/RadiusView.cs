using AntennaSystem.Data;
using DG.Tweening;
using UnityEngine;

namespace AntennaSystem
{
    public class RadiusView : MonoBehaviour
    {
        [SerializeField] private Color radiusColor;
        [SerializeField] private Color searchColor;
        [SerializeField] private float timeView;
        [SerializeField] private float endAlpha;
        private Renderer _renderer;
        private Material _material;
        private Vector2 _sizeRange;

        private bool _currentState;
        public void Init(AntennaSettings settings)
        {
            _sizeRange = new Vector2(transform.localScale.x, settings.radius * 2);
            _renderer = GetComponent<Renderer>();
            _material = new Material(_renderer.material);
            _renderer.material = _material;
            _material.color = Color.black;
            _currentState = false;
        }

        public void ChangeState(bool isEnabled = true)
        {
            if(_currentState == isEnabled)
                return;
            _currentState = isEnabled;
            DOTween.Kill(gameObject);
            var seq = DOTween.Sequence();
            seq.Append(transform.DOScale(isEnabled ? _sizeRange.y : _sizeRange.x, timeView));
            seq.Append(_material.DOColor(isEnabled ? radiusColor : Color.black, timeView));
            seq.Append(_material.DOFade(isEnabled ? endAlpha : 0, timeView));
        }

        public void ChangeColor(bool isSearch = true)
        {
            _material.DOColor(isSearch ? searchColor : radiusColor, timeView);
        }
    }
}