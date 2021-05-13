using System;
using Gameplay;
using UnityEngine;

namespace UI
{
    public class UIOverlayController : MonoBehaviour
    {
        [SerializeField] private CoinsCounterView _coinsCounter;
        [SerializeField] private LevelView _level;
        //[SerializeField] private LevelProgressView _levelProgress;

        private void Awake()
        {
            GameEventsManager.Subscribe(GameEventType.LevelLoaded, OnLevelLoaded);
            GameEventsManager.Subscribe(GameEventType.CoinObtained, OnCoinsCountChanged);
            GameEventsManager.Subscribe(GameEventType.CoinSpent, OnCoinsCountChanged);
            
            _coinsCounter.UpdateCounter($"{PlayerDataHolder.CoinsCount}");
            _level.UpdateLevel("x");
        }

        private void OnDestroy()
        {
            GameEventsManager.Unsubscribe(GameEventType.LevelLoaded, OnLevelLoaded);
            GameEventsManager.Unsubscribe(GameEventType.CoinObtained, OnCoinsCountChanged);
            GameEventsManager.Unsubscribe(GameEventType.CoinSpent, OnCoinsCountChanged);
        }

        private void OnCoinsCountChanged()
        {
            _coinsCounter.UpdateCounter($"{PlayerDataHolder.CoinsCount}");
        }

        private void OnLevelLoaded()
        {
            _level.UpdateLevel($"{PlayerDataHolder.LevelIndex}");
            //_levelProgress.Initialize(5);
        }
    }
}