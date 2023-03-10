using Unity.Mathematics;

namespace Assets._Project._Scripts.World.Generation.GenerationComponents.Terrain.Math.NoiseFilters
{
    public interface INoiseFilter
    {
        public float Evaluate(float3 point, PerlinNoiseEvaluator noiseEvaluator);
    }
}