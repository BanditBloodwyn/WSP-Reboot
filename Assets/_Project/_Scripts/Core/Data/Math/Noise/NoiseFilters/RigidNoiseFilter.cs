using System;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters
{
    [Serializable]
    public partial struct RigidNoiseFilter : INoiseFilter
    {
        [Range(0, 8)] public int NumberOfLayers;

        [Range(0, 30)] public float Strength;
        [Min(0)] public float MinValue;
        [Min(0)] public float MaxValue;
        public float2 Center;

        [Range(0, 0.01f)] public float BaseRoughness;
        [Range(0, 10)] public float Roughness;
        [Range(0, 5)] public float Persistance;
        [Range(0, 10)] public float WeightMultiplier;

        public readonly float Evaluate(float x, float y, int seed)
        {
            float noiseValue = 0;
            float frequency = BaseRoughness;
            float amplitude = 1;
            float weight = 1;

            for (int i = 0; i < NumberOfLayers; i++)
            {
                float v = 1 - Mathf.Abs(noise.snoise(new float2(x, y) * frequency + Center));

                v *= v;
                v *= weight;
                weight = Mathf.Clamp01(v * WeightMultiplier);

                noiseValue += v * amplitude;
                frequency *= Roughness;
                amplitude *= Persistance;
            }

            noiseValue *= Strength;

            noiseValue = noiseValue >= MinValue
                ? noiseValue
                : MinValue;

            noiseValue = noiseValue <= MaxValue
                ? noiseValue
                : MaxValue;

            return noiseValue - MinValue;
        }
    }
}