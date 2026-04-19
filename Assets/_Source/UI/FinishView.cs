using LevelSystem;
using UnityEngine;

namespace UI
{
    public class FinishView : WindowStateSwitcher
    {
        [SerializeField] private GameObject[] stars;

        protected override void BuildAnimation(bool isEnabled = true)
        {
            base.BuildAnimation(isEnabled);
            if (StarCounter.CurrentCountStar <= 0) 
                return;
            for (int i = 0; i < StarCounter.CurrentCountStar; i++)
            {
                stars[i].SetActive(true);
            }
        }
    }
}