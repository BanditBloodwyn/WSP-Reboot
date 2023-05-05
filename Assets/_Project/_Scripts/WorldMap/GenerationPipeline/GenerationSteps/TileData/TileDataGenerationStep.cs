using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Height;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.TileData
{
    public class TileDataGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => new() { typeof(HeightGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            foreach (Chunk chunk in context.Chunks)
            {
                foreach (Entity tile in chunk.Tiles)
                    tile.AddTileProperties(TileDataGenerator.Generate(tile, settings));

                yield return null;
            }
        }
    }
}