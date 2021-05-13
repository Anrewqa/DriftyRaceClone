using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinsCounterView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsCountTextField;

        public void UpdateCounter(string coins)
        {
            _coinsCountTextField.text = coins;
        }
    }
}