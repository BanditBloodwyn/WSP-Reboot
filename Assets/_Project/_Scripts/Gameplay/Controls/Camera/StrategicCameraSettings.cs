using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Controls.Camera
{
    [CreateAssetMenu(fileName = "StrategicCameraSettings", menuName = "ScriptableObjects/Settings/Camera/Strategic Camera Settings")]
    public class StrategicCameraSettings : ScriptableObject
    {
        [Range(1.0f, 10.0f)] public float PanningSpeed = 20.0f;
        [Range(0.01f, 1.0f)] public float RotationSpeed = 0.1f;
        [Range(0.1f, 10.0f)] public float ZoomSpeed = 2.0f;
        [Range(1.0f, 50.0f)] public float ZoomDamping = 20.0f;
        [Range(0.01f, 1.0f)] public float PitchSpeed = 0.05f;

        public AnimationCurve PanningSpeedCurve;
        public AnimationCurve ZoomingSpeedCurve;
    }
}