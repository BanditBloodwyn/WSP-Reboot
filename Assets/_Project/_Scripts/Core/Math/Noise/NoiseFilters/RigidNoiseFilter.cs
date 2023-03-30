using System;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.Core.Math.Noise.NoiseFilters
{
    [Serializable]
    public class RigidNoiseFilter : INoiseFilter
    {
        [SerializeField, Range(0,8)] private int _numberOfLayers;

        [SerializeField, Range(0, 30)] private float _strength;
        [SerializeField, Min(0)] private float _minValue;
        [SerializeField, Min(0)] private float _maxValue;
        [SerializeField] private float3 _center;

        [SerializeField, Range(0, 0.01f)] private float _baseRoughness;
        [SerializeField, Range(0, 10)] private float _roughness;
        [SerializeField, Range(0, 5)] private float _persistance;
        [SerializeField, Range(0, 10)] private float _weightMultiplier;
       
        public float Evaluate(float3 point, PerlinNoiseEvaluator noiseEvaluator, DefaultNoiseFilterSettings settings)
        {
            _numberOfLayers = settings.NumberOfLayers;
            _strength = settings.Strength;
            _minValue = settings.MinValue;
            _maxValue = settings.MaxValue;
            _center = settings.Center;
            _baseRoughness = settings.BaseRoughness;
            _roughness = settings.Roughness;
            _persistance = settings.Persistance;
            _weightMultiplier = 1;

            return Evaluate(point, noiseEvaluator);
        }

        public float Evaluate(float3 point, PerlinNoiseEvaluator noiseEvaluator)
        {
            float noiseValue = 0;
            float frequency = _baseRoughness;
            float amplitude = 1;
            float weight = 1;

            for (int i = 0; i < _numberOfLayers; i++)
            {
                float v = 1 - Mathf.Abs(noiseEvaluator.Evaluate(point * frequency + _center));

                v *= v;
                v *= weight;
                weight = Mathf.Clamp01(v * _weightMultiplier);

                noiseValue += v * amplitude;
                frequency *= _roughness;
                amplitude *= _persistance;
            }

            noiseValue *= _strength;

            noiseValue = noiseValue >= _minValue
                ? noiseValue
                : _minValue;

            noiseValue = noiseValue <= _maxValue
                ? noiseValue
                : _maxValue;

            return noiseValue - _minValue;
        }
    }
}