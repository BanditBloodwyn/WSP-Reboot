using System;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters
{
    [Serializable]
    public struct StandardNoiseFilter : INoiseFilter
    {
        [Range(0, 8)] public int NumberOfLayers;

        [Range(0, 200)] public float Strength;
        [Min(0)] public float MinValue;
        [Min(0)] public float MaxValue;
        public float3 Center;

        [Range(0, 1f)] public float BaseRoughness;
        [Range(0, 10)] public float Roughness;
        [Range(0, 5)] public float Persistance;
        public bool Rescale;

        public float Evaluate(float3 point, int seed)
        {
            PerlinNoiseEvaluator noiseEvaluator = new(seed);

            float noiseValue = 0;
            float frequency = BaseRoughness;
            float amplitude = 1;

            for (int i = 0; i < NumberOfLayers; i++)
            {
                float v = noiseEvaluator.Evaluate(point * frequency + Center);
                noiseValue += (v + 1) * 0.5f * amplitude;
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

            if(Rescale)
            {
                noiseValue -= MinValue;

                float span = MaxValue - MinValue;
                float rescaled = noiseValue / span * 100;
                noiseValue = rescaled;
            }

            return noiseValue;
        }
    }
}