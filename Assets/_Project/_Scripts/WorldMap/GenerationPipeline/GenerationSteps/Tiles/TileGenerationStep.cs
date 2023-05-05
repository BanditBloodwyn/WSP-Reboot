using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Chunks;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Tiles
{
    public class TileGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => new() { typeof(ChunkGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            foreach (Chunk chunk in context.Chunks)
            {
                List<Entity> entities = new();

                for (int x = 0; x < chunk.Size; x++)
                {
                    for (int y = 0; y < chunk.Size; y++)
                    {
                        Entity tile = TileCreator.CreateTile(
                            x + chunk.Size * chunk.Coordinates.x - chunk.Size / 2,
                            y + chunk.Size * chunk.Coordinates.y - chunk.Size / 2,
                            chunk.Coordinates.y * chunk.Size + chunk.Coordinates.x);

                        entities.Add(tile);
                    }
                }

                chunk.Tiles = entities.ToArray();

                yield return null;
            }
        }
    }
}