using Assets._Project._Scripts.World.Generation.Settings;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    [CreateAssetMenu(fileName = "CinematicCamera", menuName = "ScriptableObjects/Cameras/Cinematic Camera")]
    public class CinematicCamera : ScriptableObject, ICameraController
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private CinematicCameraSettings _settings;
        [SerializeField] private WorldCreationParameters _worldParameters;

        public void Execute()
        {
        }
    }
}