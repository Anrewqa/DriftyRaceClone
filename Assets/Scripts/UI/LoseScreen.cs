using Gameplay;
using UnityEngine;

namespace UI
{
    public class LoseScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _content;

        private void Start()
        {
            GameEventsManager.Subscribe(GameEventType.PlayerFall, OnPlayerFall);
        }
        
        private void OnDestroy()
        {
            GameEventsManager.Unsubscribe(GameEventType.PlayerFall, OnPlayerFall);
        }

        private void OnPlayerFall()
        {
            _content.SetActive(true);
        }
    }
}