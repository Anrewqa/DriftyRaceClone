using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeView : MonoBehaviour
    {
        public event Action UpgradeClicked;

        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _labelTextField;
        [SerializeField] private TextMeshProUGUI _valueTextField;
        [SerializeField] private TextMeshProUGUI _costTextField;
        [SerializeField] private Button _button;
        
        public void Initialize(string label, string value, string cost)
        {
            _labelTextField.text = label;
            _valueTextField.text = value;
            _costTextField.text = cost;
            
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public void UpdateData(string value, string cost)
        {
            _valueTextField.text = value;
            _costTextField.text = cost;
        }

        public void OnButtonClick()
        {
            UpgradeClicked?.Invoke();
        }

        public void UpdateAvailability(bool isAvailable)
        {
            _button.interactable = isAvailable;
            var color = isAvailable ? Color.white : Color.gray;
            _image.color = color;
            _labelTextField.color = color;
            _costTextField.color = color;
        }
    }
}