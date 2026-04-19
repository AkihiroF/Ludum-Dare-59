using LevelSystem;
using TMPro;
using UnityEngine;

namespace UI
{
    public class StarView : WindowStateSwitcher
    {
        [SerializeField] private TextMeshProUGUI countText;

        protected override void Awake()
        {
            base.Awake();
            StarCounter.OnCountChanged += UpdateStarCount;
        }

        private void UpdateStarCount()
        {
            ChangeState();
            countText.text = StarCounter.CurrentCountStar.ToString();
        }
    }
}