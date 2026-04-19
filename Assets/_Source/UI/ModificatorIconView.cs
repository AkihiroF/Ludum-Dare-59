using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ModificatorIconView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI countText;
        [SerializeField] private TextMeshProUGUI scaleText;

        public void UpdateData(float scale, int count)
        {
            scaleText.text = MathF.Round(scale).ToString();
            UpdateCount(count);
            ChangeState(true);
        }

        private void UpdateCount(int newValue)
        {
            countText.text = newValue.ToString();
        }

        public void ChangeState(bool isEnable)
        {
            gameObject.SetActive(isEnable);
        }
    }
}