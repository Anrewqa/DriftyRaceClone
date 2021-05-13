using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public static class GameEventsManager
    {
        private static Dictionary<GameEventType, List<Action>> _subscribers;
        
        static GameEventsManager()
        {
            InitializeDictionary();
        }

        private static void InitializeDictionary()
        {
            _subscribers = new Dictionary<GameEventType, List<Action>>();
            
            foreach (GameEventType gameEventType in Enum.GetValues(typeof(GameEventType)))
            {
                _subscribers[gameEventType] = new List<Action>();
            }
        }
        
        public static void CallEvent(GameEventType gameEvent)
        {
            foreach (var action in _subscribers[gameEvent])
            {
                action?.Invoke();
            }
        }

        public static void Subscribe(GameEventType type, Action action)
        {
            _subscribers[type].Add(action);
        }
        
        public static void Unsubscribe(GameEventType type, Action action)
        {
            _subscribers[type].Remove(action);
        }
        
        public static void UnsubscribeAll()
        {
            foreach (GameEventType gameEventType in Enum.GetValues(typeof(GameEventType)))
            {
                _subscribers[gameEventType].Clear();
            }
        }
    }
}