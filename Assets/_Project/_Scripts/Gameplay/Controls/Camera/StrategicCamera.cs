using Assets._Project._Scripts.Core.Extentions;
using Assets._Project._Scripts.Gameplay.Helper;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using NUnit.Framework;
using Sirenix.Utilities;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    [CreateAssetMenu(fileName = "StrategicCamera", menuName = "ScriptableObjects/Cameras/Strategic Camera")]
    public class StrategicCamera : ScriptableObject, ICameraController
    {
        [SerializeField] private StrategicCameraSettings _settings;
        [SerializeField] private WorldCreationParameters _worldParameters;

        private Transform _cameraTransform;

        private void Awake()
        {
            Assert.IsNotNull(_settings);
            Assert.IsNotNull(_worldParameters);
        }

        public void ResetController(CameraHandler cameraHandler)
        {
            _cameraTransform = cameraHandler.transform;
            Assert.IsNotNull(_cameraTransform);
        }

        public void Execute()
        {
            HandleRotation();
            HandlePosition();
            HandleZoom();
        }

        private void HandleRotation()
        {
            Cursor.visible = !Input.GetMouseButton(2);

            if (Input.GetMouseButton(2) == false)
                return;

            float yaw = Input.GetAxis("Mouse X") * _settings.YawSpeed * Time.deltaTime;
            float pitchDelta = Input.GetAxis("Mouse Y") * _settings.PitchSpeed * Time.deltaTime;

            if (_settings.InversePitch)
                pitchDelta *= -1;
            if (_settings.InverseYaw)
                yaw *= -1;

            _cameraTransform.RotateAround(PointerHelper.GetCurrentViewPosition(), Vector3.up, yaw);
           
            float pitch = _cameraTransform.rotation.eulerAngles.x;

            if((pitch > 20 || pitchDelta > 0) && (pitch < 80 || pitchDelta < 0)) 
                _cameraTransform.Rotate(Vector3.right, pitchDelta);
        }

        private void HandlePosition()
        {
            if (!Input.GetMouseButton(1))
                return;

            float lookHorizontal = -Input.GetAxis("Mouse X");
            float lookVertical = -Input.GetAxis("Mouse Y");

            Vector3 translation = _cameraTransform.forward.Get2D() * lookVertical + _cameraTransform.right.Get2D() * lookHorizontal;

            float speedFromHeight = _settings.PanningSpeedCurve.Evaluate(_cameraTransform.position.y);

            float chunkCountPerAxis = _worldParameters.ChunkCountPerAxis;
            float tileAmountPerAxis = _worldParameters.TileAmountPerAxis;

            _cameraTransform.position += _settings.PanningSpeed * speedFromHeight * Time.deltaTime * translation;
            _cameraTransform.position = _cameraTransform.position.Clamp(
                new Vector3(-tileAmountPerAxis / 2.0f, 3, -tileAmountPerAxis / 2.0f),
                new Vector3(chunkCountPerAxis * tileAmountPerAxis - tileAmountPerAxis / 2, 1000, chunkCountPerAxis * tileAmountPerAxis - tileAmountPerAxis / 2));
        }

        private void HandleZoom()
        {
            if (Mathf.Abs(Input.mouseScrollDelta.y) == 0)
                return;

            float speedFromHeight = _settings.ZoomingSpeedCurve.Evaluate(_cameraTransform.position.y);

            Vector3 zoomDirection = _settings.ZoomSpeed * speedFromHeight * Time.deltaTime * _cameraTransform.forward;

            if (Input.mouseScrollDelta.y > 0)
                _cameraTransform.position += zoomDirection;
            if (Input.mouseScrollDelta.y < 0)
                _cameraTransform.position -= zoomDirection;
        }
    }
}