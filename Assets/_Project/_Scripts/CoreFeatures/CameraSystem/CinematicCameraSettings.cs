using UnityEngine;

namespace Assets._Project._Scripts.CoreFeatures.CameraSystem
{
    [CreateAssetMenu(fileName = "CinematicCameraSettings", menuName = "ScriptableObjects/Settings/Camera/Cinematic Camera Settings")]
    public class CinematicCameraSettings : ScriptableObject
    {
        [Range(5, 20)] public float ShotDuration;
        [Range(5, 20)] public float MaximumShotDistance;
       
        [Range(10, 100)] public float MinimumHeight;
        [Range(10, 100)] public float MaximumHeight;

    }
}