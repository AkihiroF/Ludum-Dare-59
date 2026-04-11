using TMPro;
using UnityEngine;

public class InputTester : MonoBehaviour
{
        [SerializeField] private TextMeshProUGUI moveOutput;
        [SerializeField] private TextMeshProUGUI lookOutput;

        public void UpdateMove(Vector2 move)
        {
                moveOutput.text = move.ToString();
        }
        public void UpdateLook(Vector2 look)
        {
                lookOutput.text = look.ToString();
        }
}