using Assets._Project._Scripts.Core.Math.Noise;
using Assets._Project._Scripts.Core.Math.Noise.NoiseFilters;
using Assets._Project._Scripts.WorldMap.Data.Structs.ComponentData;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.TileData
{
    public static class ResourceGenerator
    {
        public static ResourceValues Generate(
            TilePropertiesComponentData data, 
            float3 position, 
            WorldCreationParameters settings)
        {
            PerlinNoiseEvaluator evaluator = new PerlinNoiseEvaluator();
            StandardNoiseFilter noiseFilter = new StandardNoiseFilter();

            ResourceValues resourceValues = new ResourceValues();
            resourceValues.Coal = GenerateCoal(position, evaluator, noiseFilter);
            resourceValues.IronOre = GenerateIronOre(position, evaluator, noiseFilter);
            resourceValues.GoldOre = GenerateGoldOre(position, evaluator, noiseFilter);
            resourceValues.Oil = GenerateOil(position);
            resourceValues.Gas = GenerateGas(position);

            return resourceValues;
        }

        private static float GenerateCoal(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter)
        {
            return 0;
        }

        private static float GenerateIronOre(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter)
        {
            return 0;
        }

        private static float GenerateGoldOre(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter)
        {
            return 0;
        }

        private static float GenerateOil(float3 position)
        {
            return 0;
        }

        private static float GenerateGas(float3 position)
        {
            return 0;
        }
    }
}