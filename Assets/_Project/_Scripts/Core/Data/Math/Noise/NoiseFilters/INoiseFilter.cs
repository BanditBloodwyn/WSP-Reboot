using Unity.Mathematics;

namespace Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters
{
    public interface INoiseFilter
    {
        public float Evaluate(float3 point, int seed);
    }
}