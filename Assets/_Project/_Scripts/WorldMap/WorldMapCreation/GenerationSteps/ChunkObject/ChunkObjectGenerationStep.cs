using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Mesh;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.ChunkObject
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