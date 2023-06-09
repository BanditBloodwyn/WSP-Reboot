using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.Core.EventSystem
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent")]
    public class GameEventSO : ScriptableObject
    {
        private readonly List<GameEventListener> _listeners = new();

        public void Raise(Component sender, object data)
        {
            foreach (GameEventListener listener in _listeners)
                listener.OnEventRaised(sender, data);
        }

        public void RegisterListener(GameEventListener listener)
        {
            if(!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if( _listeners.Contains(listener))
                _listeners.Remove(listener);
        }
    }
}
