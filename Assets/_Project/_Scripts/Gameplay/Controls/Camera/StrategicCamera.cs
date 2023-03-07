using Sirenix.Utilities;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    public class StrategicCamera : MonoBehaviour
    {
        private bool _enabled;
        
        [SerializeField] private StrategicCameraSettings _settings;

        #region Unity

        private void Start()
        {
        }

        private void Update()
        {
            if (!_enabled)
                return;

            HandleRotation();
            HandlePosition();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1.0f);
        }

        #endregion

        #region EventResponses

        public void Activate()
        {
            Debug.Log("<color=green>Activate StrategicCamera");
          
            _enabled = true;
        }

        public void Deactivate()
        {
            Debug.Log("Deactivate StrategicCamera");

            _enabled = false;
        }

        #endregion


        #region Control

        private void HandleRotation()
        {
            Cursor.visible = !Input.GetMouseButton(2);
            Cursor.lockState = Input.GetMouseButton(2) ? CursorLockMode.Locked : CursorLockMode.None;

            if (Input.GetMouseButton(2) == false)
                return;

            float lookHorizontal = Input.GetAxis("Mouse X");
            float rotationY = lookHorizontal * _settings.RotationSpeed;

            transform.eulerAngles += new Vector3(0, rotationY, 0);
        }

        private void HandlePosition()
        {
            if (!Input.GetMouseButton(1))
                return;

            float lookHorizontal = -Input.GetAxis("Mouse X");
            float lookVertical = -Input.GetAxis("Mouse Y");

            Vector3 translation = transform.forward * lookVertical + transform.right * lookHorizontal;

            transform.position += _settings.PanningSpeed * Time.deltaTime * translation;
            transform.position = transform.position.Clamp(new Vector3(0, 0, 0), new Vector3(float.PositiveInfinity, 0, float.PositiveInfinity));
        }

        #endregion
    }
}
