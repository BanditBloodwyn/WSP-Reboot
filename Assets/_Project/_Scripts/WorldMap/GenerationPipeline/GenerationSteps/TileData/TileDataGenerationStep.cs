using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Height;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.Features.WorldMap.WorldMapCore.Types;
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