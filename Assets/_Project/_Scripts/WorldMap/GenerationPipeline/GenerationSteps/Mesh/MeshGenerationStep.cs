using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Height;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Mesh
{
    public class MeshGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => new() { typeof(HeightGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            foreach (Chunk chunk in context.Chunks)
            {
                List<float3> tilePositions = new();

                foreach (Entity tile in chunk.Tiles)
                {
                    EmptyTileAspect tileAspect = tile.GetEmptyTileAspect();
                    tilePositions.Add(tileAspect.Position);
                }

                chunk.Mesh = MeshGenerator.GenerateMeshFromVoxelPositions(
                    chunk,
                    tilePositions.ToArray());

                yield return null;
            }
        }
    }
}