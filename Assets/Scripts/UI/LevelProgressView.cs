using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  UI
{
    public class LevelProgressView : MonoBehaviour
    {
        [SerializeField] private Image _levelProgressBoxPrefab;

        private int _checkpointsCount;
        private readonly List<Image> _boxes = new List<Image>();

        public void Initialize(int checkpointsCount)
        {
            _boxes.Clear();
            _checkpointsCount = checkpointsCount;
            
            for (int i = 0; i < checkpointsCount; i++)
            {
                var rectTransform = GetComponent<RectTransform>();
                var width = rectTransform.rect.width / checkpointsCount;
                var box = Instantiate(_levelProgressBoxPrefab, rectTransform);
                box.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                _boxes.Add(box);
            }
        }

        public void UpdateLevelProgress(float levelProgress)
        {
            for (var i = 0; i < levelProgress * _checkpointsCount && i < _checkpointsCount; i++)
            {
                _boxes[i].enabled = true;
            }
        }
    }
}