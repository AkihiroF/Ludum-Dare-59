using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class ModificationView : WindowStateSwitcher
    {
        [SerializeField] private ModificatorIconView modificationIconView;
        [SerializeField] private Transform[] pointsForMovement;

        public void ChangeCount(float cvpScale, int newValue)
        {
            ChangeState(newValue > 0);
            modificationIconView.UpdateData(cvpScale, newValue);
        }
        protected override void BuildAnimation(bool isEnabled = true)
        {
            var path = isEnabled ? GetPathForward() : GetPathBackward();
            Tween = transform
                .DOPath(path, timeSwitching, PathType.CatmullRom).OnComplete(() => ChangeInteractive(isEnabled));
            Tween.OnStart(() => TargetPanel.DOFade(isEnabled ? 1f : 0f, timeSwitching));
        }

        private Vector3[] GetPathForward()
        {
            Vector3[] path = new Vector3[pointsForMovement.Length];
            for (int i = 0; i < pointsForMovement.Length; i++)
            {
                path[i] = pointsForMovement[i].position;
            }
            return path;
        }
        private Vector3[] GetPathBackward()
        {
            int len = pointsForMovement.Length;
            Vector3[] path = new Vector3[len];
            for (int i = 0; i < len; i++)
            {
                path[i] = pointsForMovement[len - 1 - i].position;
            }
            return path;
        }
    }
}