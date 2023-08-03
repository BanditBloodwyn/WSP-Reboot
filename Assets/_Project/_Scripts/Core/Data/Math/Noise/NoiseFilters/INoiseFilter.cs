namespace Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters
{
    [PolymorphicStruct]
    public interface INoiseFilter
    {
        public float Evaluate(float x, float y, int seed);
    }
}