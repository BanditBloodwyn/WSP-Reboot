using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Height;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.TileData
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