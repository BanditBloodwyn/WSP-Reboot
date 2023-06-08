using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Chunks;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Tiles
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