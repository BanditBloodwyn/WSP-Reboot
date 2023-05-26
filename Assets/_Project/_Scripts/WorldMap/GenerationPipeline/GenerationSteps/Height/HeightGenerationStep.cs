using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Tiles;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using AdvancedNoiseLib;
using Unity.Entities;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Height
{
    public class HeightGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => new() { typeof(TileGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            NoiseEvaluatorBuilder noiseEvaluatorBuilder = new NoiseEvaluatorBuilder();

            INoiseEvaluator noiseEvaluator = noiseEvaluatorBuilder
                .WithSeed(0)
                .WithSettings(settings.NoiseSettingJSON.text)
                .ToUseFirstFilterLayerAsMask(true)
                .Build();

            foreach (Chunk chunk in context.Chunks)
            {
                foreach (Entity tileEntity in chunk.Tiles)
                {
                    EmptyTileAspect tileAspect = tileEntity.GetEmptyTileAspect();

                    float height = noiseEvaluator.Evaluate2D(tileAspect.Position.x, tileAspect.Position.z);
                    tileAspect.Entity.SetTilePosition(new float3(tileAspect.Position.x, height, tileAspect.Position.z));
                }

                yield return null;
            }
        }
    }
}