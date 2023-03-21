using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    [CreateAssetMenu(fileName = "CinematicCameraSettings", menuName = "ScriptableObjects/Settings/Camera/Cinematic Camera Settings")]
    public class CinematicCameraSettings : ScriptableObject
    {
        [Range(5, 20)] public float ShotDuration;
        [Range(5, 20)] public float MaximumShotDistance;
    }
}