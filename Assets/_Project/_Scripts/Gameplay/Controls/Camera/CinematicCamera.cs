using System.Collections;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    [CreateAssetMenu(fileName = "CinematicCamera", menuName = "ScriptableObjects/Cameras/Cinematic Camera")]
    public class CinematicCamera : ScriptableObject, ICameraController
    {
        [SerializeField] private CinematicCameraSettings _settings;
        [SerializeField] private WorldCreationParameters _worldParameters;
        
        private Transform _cameraTransform;
        private CameraHandler _cameraHandler;

        private float _elapsedTime;
        private bool _started;
      
        private void Awake()
        {
            _started = false;
            Assert.IsNotNull(_settings);
            Assert.IsNotNull(_worldParameters);
        }

        public void ResetController(CameraHandler cameraHandler)
        {
            _cameraHandler = cameraHandler;
            _cameraTransform = cameraHandler.transform;
           
            Assert.IsNotNull(_cameraHandler);
            Assert.IsNotNull(_cameraTransform);

            _cameraHandler.StopAllCoroutines();
            _started = false;
           
            Debug.Log("Reset Cinematics");
        }

        public void Execute()
        {
            if(!_started)
            {
                Debug.Log("Start Cinematics");

                _cameraHandler.StartCoroutine(Move());
            }
        }

        private IEnumerator Move()
        {
            _started = true;

            while (true)
            {
                Debug.Log("Recalculate Cinematics");
                Vector3 startPosition = CalculateStartPosition();
                Vector3 endPosition = CalculateEndPosition(startPosition);

                _cameraTransform.rotation = Quaternion.Euler(Random.Range(5, 60), Random.Range(0, 360), 0);

                _elapsedTime = 0;

                while (_elapsedTime <= _settings.ShotDuration)
                {
                    _elapsedTime += Time.deltaTime;
                    float percentageCompleted = _elapsedTime / _settings.ShotDuration;

                    _cameraTransform.position = Vector3.Lerp(
                        startPosition,
                        endPosition,
                        percentageCompleted);

                    yield return null;
                }

                _cameraTransform.position = endPosition;
            }
        }

        private Vector3 CalculateStartPosition()
        {
            return new Vector3(
                Random.Range(0, (_worldParameters.ChunkCountPerAxis - 1) * _worldParameters.TileAmountPerAxis),
                Random.Range(_settings.MinimumHeight, _settings.MaximumHeight),
                Random.Range(0, (_worldParameters.ChunkCountPerAxis - 1) * _worldParameters.TileAmountPerAxis));
        }

        private Vector3 CalculateEndPosition(Vector3 startPosition)
        {
            Vector3 potentialEndPosition = new Vector3(
                Random.Range(0, (_worldParameters.ChunkCountPerAxis - 1) * _worldParameters.TileAmountPerAxis),
                Random.Range(_settings.MinimumHeight, _settings.MaximumHeight),
                Random.Range(0, (_worldParameters.ChunkCountPerAxis - 1) * _worldParameters.TileAmountPerAxis));

            if (Vector3.Distance(startPosition, potentialEndPosition) > _settings.MaximumShotDistance)
            {
                Vector3 direction = (potentialEndPosition - startPosition).normalized;
                potentialEndPosition = startPosition + direction * _settings.MaximumShotDistance;
            }

            return potentialEndPosition;
        }
    }
}