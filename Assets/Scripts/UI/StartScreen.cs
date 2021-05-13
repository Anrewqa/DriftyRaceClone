using Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StartScreen : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
    {
        [SerializeField] private MonoBehaviour _inputController;
        [SerializeField] private GameObject _content;

        private bool _isInteractable = true;
        
        private static void StartRace()
        {
            GameEventsManager.CallEvent(GameEventType.RaceStarted);
        }

        private void Awake()
        {
            GameEventsManager.Subscribe(GameEventType.LevelLoaded, OnLevelLoaded);
        }

        private void OnDestroy()
        {
            GameEventsManager.Unsubscribe(GameEventType.LevelLoaded, OnLevelLoaded);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log($"Pointer up on start screen _isInteractable : {_isInteractable}");
            
            if (!_isInteractable)
            {
                return;
            }
            
            StartRace();
            SetScreenContentActive(false);
            _isInteractable = false;

            _inputController.enabled = true;
        }

        private void OnLevelLoaded()
        {
            SetScreenContentActive(true);
            _isInteractable = true;
            _inputController.enabled = false;
        }

        private void SetScreenContentActive(bool isActive)
        {
            _content.SetActive(isActive);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}