namespace Assets._Project._Scripts.World.Generation.GenerationComponents
{
    public interface IGenerationComponent
    {
        public bool Enabled { get; }

        public abstract void Apply(Chunk chunk);
    }
}