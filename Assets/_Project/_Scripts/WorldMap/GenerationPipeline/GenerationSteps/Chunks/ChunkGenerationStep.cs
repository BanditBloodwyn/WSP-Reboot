using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Chunks
{
    public class ChunkGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => null;

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            Landscape.Instance.Chunks.Clear();

            for (int x = 0; x < settings.WorldSize.ChunkCountPerAxis; x++)
            {
                for (int y = 0; y < settings.WorldSize.ChunkCountPerAxis; y++)
                {
                    Chunk chunk = new();
                    chunk.ID = y * settings.WorldSize.ChunkCountPerAxis + x;
                    chunk.Coordinates = new Vector2Int(x, y);
                    chunk.Size = settings.WorldSize.TileAmountPerAxis;

                    Landscape.Instance.Chunks.Add(chunk);
                    context.Chunks.Add(chunk);

                    yield return null;
                }
            }
        }
    }
}