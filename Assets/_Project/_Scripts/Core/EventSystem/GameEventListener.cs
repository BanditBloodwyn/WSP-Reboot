using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project._Scripts.Core.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEventSO _gameEvent;
        [SerializeField] private UnityEvent _response;

        private void OnEnable()
        {
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            _response.Invoke();
        }
    }
}
