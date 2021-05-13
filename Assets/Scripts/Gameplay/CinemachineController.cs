using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Animator))]
    public class CinemachineController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int _gameTrigger = Animator.StringToHash("Game");
        private static readonly int _introTrigger = Animator.StringToHash("Intro");
        private static readonly int _endTrigger = Animator.StringToHash("End");

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            GameEventsManager.Subscribe(GameEventType.RaceStarted, SetGameTrigger);
            GameEventsManager.Subscribe(GameEventType.RaceEnded, SetEndTrigger);
            GameEventsManager.Subscribe(GameEventType.LevelLoaded, SetIntroTrigger);
        }

        private void OnDestroy()
        {
            GameEventsManager.Unsubscribe(GameEventType.RaceStarted, SetGameTrigger);
            GameEventsManager.Unsubscribe(GameEventType.RaceStarted, SetGameTrigger);
            GameEventsManager.Unsubscribe(GameEventType.LevelLoaded, SetIntroTrigger);
        }

        private void SetGameTrigger()
        {
            _animator.SetTrigger(_gameTrigger);
        }
        
        private void SetEndTrigger()
        {
            _animator.SetTrigger(_endTrigger);
        }

        private void SetIntroTrigger()
        {
            _animator.SetTrigger(_introTrigger);
        }
    }
}