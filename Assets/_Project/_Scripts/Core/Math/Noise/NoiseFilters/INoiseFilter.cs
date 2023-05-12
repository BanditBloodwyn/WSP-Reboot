using Unity.Mathematics;

namespace Assets._Project._Scripts.Core.Math.Noise.NoiseFilters
{
    public interface INoiseFilter
    {
        public float Evaluate(float3 point, int seed);
    }
}