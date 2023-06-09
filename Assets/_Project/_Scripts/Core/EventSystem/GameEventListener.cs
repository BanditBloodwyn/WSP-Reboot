using UnityEngine;

namespace Assets._Project._Scripts.Core.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEventSO _gameEvent;
        [SerializeField] private CustomGameEvent _response;

        private void OnEnable()
        {
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            _response.Invoke(sender, data);
        }
    }
}
