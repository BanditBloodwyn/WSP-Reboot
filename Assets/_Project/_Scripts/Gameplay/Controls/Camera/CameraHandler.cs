using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
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
                cameraController.ResetController();
        }

        private void Update()
        {
            if (!_enabled)
                return;

            if (_cameraType is not ICameraController cameraController)
                return;

            cameraController.Execute();
        }

        #endregion

        #region EventResponses

        public void Activate()
        {
            Debug.Log("<color=#73BD73>Activate StrategicCamera</color>");

            _enabled = true;
        }

        public void Deactivate()
        {
            Debug.Log("<color=orange>Deactivate StrategicCamera</color>");

            _enabled = false;
        }

        #endregion
    }
}