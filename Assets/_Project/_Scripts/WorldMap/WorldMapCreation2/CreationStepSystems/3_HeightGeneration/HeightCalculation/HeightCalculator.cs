using Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters;
using Unity.Burst;
using Unity.Collections;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._3_HeightGeneration.HeightCalculation
{
    [BurstCompile]
    public struct HeightCalculator
    {
        [ReadOnly] private NativeArray<NoiseFilter> _noiseFilters;

        public HeightCalculator(NativeArray<NoiseFilter> noiseFilters)
        {
            _noiseFilters = noiseFilters;
        }

        [BurstCompile]
        public float Evaluate(float x, float y)
        {
            float firstLayerValue = 0;
            float elevation = 0;

            if (_noiseFilters.Length > 0)
            {
                firstLayerValue = _noiseFilters[0].Evaluate(x, y, 0);
                elevation = firstLayerValue;
            }

            if (_noiseFilters.Length == 1)
                return elevation;

            for (int i = 1; i < _noiseFilters.Length; i++)
                elevation += _noiseFilters[i].Evaluate(x, y, 0) * firstLayerValue;

            return elevation;
        }
    }
}