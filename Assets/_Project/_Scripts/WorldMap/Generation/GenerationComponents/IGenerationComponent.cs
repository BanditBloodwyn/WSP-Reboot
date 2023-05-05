using Assets._Project._Scripts.WorldMap.GenerationPipeline;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents
{
    public interface IGenerationComponent
    {
        public bool Enabled { get; }

        public abstract void Apply(Chunk chunk);
    }
}