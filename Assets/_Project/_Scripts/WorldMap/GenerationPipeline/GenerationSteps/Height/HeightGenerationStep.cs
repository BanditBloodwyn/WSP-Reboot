using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Tiles;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using AdvancedNoiseLib;
using AdvancedNoiseLib.Math.Noise;
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
            NoiseEvaluatorBuilder noiseEvaluatorBuilder = new NoiseEvaluatorBuilder();
            INoiseEvaluator noiseEvaluator = noiseEvaluatorBuilder
                .WithSeed(0)
                .WithSettings("[\r\n{\r\nType : \"Standard\",\r\nNumberOfLayers : 3,\r\nCenterX : 0,\r\nCenterY : 0,\r\nCenterZ : 0,\r\nMinimum : 4.317789291882556,\r\nMaximum : 100,\r\nStrength : 2.6079447322970637,\r\nBaseRoughness : 0.0017443868739205528,\r\nRoughness : 0.17443868739205526,\r\nPersistance : 1\r\n}\r\n,\r\n{\r\nType : \"Rigid\",\r\nNumberOfLayers : 4,\r\nCenterX : 0,\r\nCenterY : 0,\r\nCenterZ : 0,\r\nMinimum : 0,\r\nMaximum : 100,\r\nStrength : 6.403889514058062,\r\nBaseRoughness : 0.0013754271416853185,\r\nRoughness : 1.5336787564766843,\r\nPersistance : 1,\r\nWeightMultiplier : 1\r\n}\r\n,\r\n\r\n]")
                .ToUseFirstFilterLayerAsMask(true)
                .Build();
            PerlinNoiseEvaluator evaluator = new PerlinNoiseEvaluator();

            foreach (Chunk chunk in context.Chunks)
            {
                foreach (Entity tileEntity in chunk.Tiles)
                {
                    EmptyTileAspect tileAspect = tileEntity.GetEmptyTileAspect();

                    //float height = CalculateHeight(tileAspect.Position.x, tileAspect.Position.z);
                    
                    float height = noiseEvaluator.Evaluate2D(tileAspect.Position.x, tileAspect.Position.z, evaluator);
                    tileAspect.Entity.SetTilePosition(new float3(tileAspect.Position.x, height, tileAspect.Position.z));
                }

                yield return null;
            }
        }
    }
}