using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Chunks
{
    public class ChunkGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => null;

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            Landscape.Instance.Chunks.Clear();

            for (int x = 0; x < settings.ChunkCountPerAxis; x++)
            {
                for (int y = 0; y < settings.ChunkCountPerAxis; y++)
                {
                    Chunk chunk = new();
                    chunk.ID = y * settings.ChunkCountPerAxis + x;
                    chunk.Coordinates = new Vector2Int(x, y);
                    chunk.Size = settings.TileAmountPerAxis;

                    Landscape.Instance.Chunks.Add(chunk);
                    context.Chunks.Add(chunk);
                    
                    yield return null;
                }
            }
        }
    }
}