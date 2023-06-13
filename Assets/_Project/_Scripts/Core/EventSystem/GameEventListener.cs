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
            string targetName = _response.GetPersistentTarget(0) is { } target
                ? target.name 
                : _response.GetPersistentTarget(0).ToString();
            Debug.Log($"<color=#FF7300>Event received</color>\nSender: {sender.name}, Response: {targetName}.<b>{_response.GetPersistentMethodName(0)}()</b>");

            _response.Invoke(sender, data);
        }
    }
}
