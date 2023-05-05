using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Mesh;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.ChunkObject
{
    public class ChunkObjectGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => new() { typeof(MeshGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            foreach (Chunk chunk in context.Chunks)
            {
                ChunkObjectGenerator.Generate(chunk, settings);
                yield return null;
            }
        }
    }
}