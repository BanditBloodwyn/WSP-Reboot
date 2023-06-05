using UnityEngine;

namespace Assets._Project._Scripts.Features.CameraSystem
{
    [CreateAssetMenu(fileName = "StrategicCameraSettings", menuName = "ScriptableObjects/Settings/Camera/Strategic Camera Settings")]
    public class StrategicCameraSettings : ScriptableObject
    {
        [Range(1.0f, 10.0f)] public float PanningSpeed = 20.0f;
        [Range(0.1f, 5.0f)] public float YawSpeed = 4.0f;
        [Range(0.1f, 5.0f)] public float PitchSpeed = 4.0f;
        [Range(0.1f, 300.0f)] public float ZoomSpeed = 2.0f;

        public AnimationCurve PanningSpeedCurve;
        public AnimationCurve ZoomingSpeedCurve;

        public bool InversePitch;
        public bool InverseYaw;
    }
}