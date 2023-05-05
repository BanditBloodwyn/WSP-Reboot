using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Tiles;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Height
{
    public class HeightGenerationStep : IWorldGenerationStep
    {
        private Random t = Random.CreateFromIndex(0);
        public List<Type> RequiredDependencies => new() { typeof(TileGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            foreach (Chunk chunk in context.Chunks)
            {
                foreach (Entity tileEntity in chunk.Tiles)
                {
                    EmptyTileAspect tileAspect = tileEntity.GetEmptyTileAspect();

                    float height = CalculateHeight(tileAspect.Position.x, tileAspect.Position.z);
                    tileAspect.Entity.SetTilePosition(new float3(tileAspect.Position.x, height, tileAspect.Position.z));
                }

                yield return null;
            }
        }

        private float CalculateHeight(float x, float y)
        {
            return t.NextFloat(0, 3);
        }
    }
}