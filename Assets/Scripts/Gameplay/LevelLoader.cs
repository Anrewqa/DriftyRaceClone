using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _levels = new List<GameObject>();

        private GameObject _currentLevel;
        
        private void Start()
        {
            GameEventsManager.Subscribe(GameEventType.LevelRequested, OnLevelRequested);
            LoadLevel();
        }

        private void OnDestroy()
        {
            GameEventsManager.Unsubscribe(GameEventType.LevelRequested, OnLevelRequested);
        }

        public void LoadNextLevelRequest()
        {
            PlayerDataHolder.ChangeLastLevelIndex(PlayerDataHolder.LevelIndex + 1);
            GameEventsManager.CallEvent(GameEventType.LevelRequested);
        }
        
        public void ReloadLevelRequest()
        {
            GameEventsManager.CallEvent(GameEventType.LevelRequested);
        }

        private void OnLevelRequested()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            Destroy(_currentLevel);
            var index = Mathf.Min(_levels.Count - 1, PlayerDataHolder.LevelIndex);
            var level = _levels[index];
            _currentLevel = Instantiate(level);
            
            GameEventsManager.CallEvent(GameEventType.LevelLoaded);
        }
    }
}