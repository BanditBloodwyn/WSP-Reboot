using UnityEngine;

namespace Assets._Project._Scripts.Core.Math.Noise
{
    public struct DefaultNoiseFilterSettings
    {
        public int NumberOfLayers;
        public float Strength;
        public float MinValue;
        public float MaxValue;
        public Vector3 Center;

        public float BaseRoughness;
        public float Roughness;
        public float Persistance;
    }
}