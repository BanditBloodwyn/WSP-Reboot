using Assets._Project._Scripts.Core.EventSystem;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.CoreFeatures.CameraSystem
{
    public class CameraHandler : MonoBehaviour
    {
        private bool _enabled;

        [SerializeField] private ScriptableObject _cameraType;

        #region Unity

        private void OnEnable()
        {
            Events.OnWorldCreationFinished.AddListener(Activate);
        }

        private void OnDisable()
        {
            Events.OnWorldCreationFinished.RemoveListener(Activate);
        }

        private void Awake()
        {
            Assert.IsNotNull(_cameraType);
        }

        private void Start()
        {

            if (_cameraType is ICameraController cameraController)
                cameraController.ResetController(this);
        }

        private void Update()
        {
            if (!_enabled)
                return;

            if (_cameraType is not ICameraController cameraController)
                return;

            if (Input.GetKeyUp(KeyCode.Space))
                cameraController.ResetController(this);
            else 
                cameraController.Execute();
        }

        #endregion

        #region EventResponses

        private object Activate(Component component, object o)
        {
            Debug.Log("<color=#73BD73>Camera</color> - <color=#73BD73>Activate</color> StrategicCamera");

            _enabled = true;

            return null;
        }

        public void Deactivate()
        {
            Debug.Log("<color=#73BD73>Camera</color> - <color=orange>Deactivate</color> StrategicCamera");

            _enabled = false;
        }

        #endregion
    }
}