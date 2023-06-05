using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.Features.CameraSystem
{
    public class CameraHandler : MonoBehaviour
    {
        private bool _enabled;

        [SerializeField] private ScriptableObject _cameraType;

        #region Unity

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

        public void Activate()
        {
            Debug.Log("<color=#73BD73>Camera</color> - <color=#73BD73>Activate</color> StrategicCamera");

            _enabled = true;
        }

        public void Deactivate()
        {
            Debug.Log("<color=#73BD73>Camera</color> - <color=orange>Deactivate</color> StrategicCamera");

            _enabled = false;
        }

        #endregion
    }
}