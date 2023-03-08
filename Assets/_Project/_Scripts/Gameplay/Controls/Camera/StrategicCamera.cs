using Assets._Project._Scripts.World.Generation.old;
using Cinemachine;
using NUnit.Framework;
using Sirenix.Utilities;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    public class StrategicCamera : MonoBehaviour
    {
        private bool _enabled;
        
        [SerializeField] private StrategicCameraSettings _settings;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private WorldCreationParameters _worldParameters;

        private Vector3 _followOffset;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_settings);
            Assert.IsNotNull(_virtualCamera);
            Assert.IsNotNull(_worldParameters);
        }

        private void Start()
        {
            _followOffset = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }

        private void Update()
        {
            if (!_enabled)
                return;

            HandleRotation();
            HandlePosition();
            HandleZoom();
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
            transform.position = transform.position.Clamp(
                new Vector3(
                    -_worldParameters.ChunkSize / 2.0f,
                    0,
                    -_worldParameters.ChunkSize / 2.0f),
                new Vector3(
                    _worldParameters.WorldSize * _worldParameters.ChunkSize - _worldParameters.ChunkSize / 2,
                    0,
                    _worldParameters.WorldSize * _worldParameters.ChunkSize - _worldParameters.ChunkSize / 2));
        }

        private void HandleZoom()
        {
            Vector3 zoomDirection = _followOffset.normalized * _settings.ZoomSpeed;

            if (Input.mouseScrollDelta.y > 0)
            {
                if((_followOffset - zoomDirection).y > 5) 
                    _followOffset -= zoomDirection;
            }            
            if (Input.mouseScrollDelta.y < 0)
            {
                _followOffset += zoomDirection;
            }

            _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(
                _virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset,
                _followOffset,
                _settings.ZoomDamping * Time.deltaTime);
        }

        #endregion
    }
}
