using Gameplay;
using UnityEngine;

namespace UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        
        private void Start()
        {
            GameEventsManager.Subscribe(GameEventType.RaceEnded, OnRaceEnded);
        }

        private void OnDestroy()
        {
            GameEventsManager.Unsubscribe(GameEventType.RaceEnded, OnRaceEnded);
        }

        private void OnRaceEnded()
        {
            _content.SetActive(true);
        }
    }
}