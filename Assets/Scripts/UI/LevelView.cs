using TMPro;
using UnityEngine;

namespace  UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelTextField;

        public void UpdateLevel(string levelIndex)
        {
            _levelTextField.text = "Level " + levelIndex;
        }
    }
}