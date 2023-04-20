using Assets._Project._Scripts.Core.Math.Noise;
using Assets._Project._Scripts.Core.Math.Noise.NoiseFilters;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.TileData
{
    [CreateAssetMenu(fileName = "ResourceGenerator", menuName = "ScriptableObjects/Settings/World/Generators/Resource Generator")]
    public class ResourceGenerator : ScriptableObject
    {
        public ResourceValues Generate(
            TilePropertiesComponentData data,
            float3 position)
        {
            PerlinNoiseEvaluator evaluator = new PerlinNoiseEvaluator();
            StandardNoiseFilter noiseFilter = new StandardNoiseFilter();

            ResourceValues resourceValues = new ResourceValues();
            resourceValues.Coal = GenerateCoal(position, evaluator, noiseFilter);
            resourceValues.IronOre = GenerateIronOre(position, evaluator, noiseFilter);
            resourceValues.GoldOre = GenerateGoldOre(position, evaluator, noiseFilter);
            resourceValues.Oil = GenerateOil(position, evaluator, noiseFilter);
            resourceValues.Gas = GenerateGas(position, evaluator, noiseFilter);

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

        private static float GenerateOil(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter)
        {
            DefaultNoiseFilterSettings filterSettings = new DefaultNoiseFilterSettings
            {
                NumberOfLayers = 1,
                Strength = 200,
                MinValue = 100,
                MaxValue = 100,
                Center = new Vector3(0, 0, 0),
                BaseRoughness = 0.005f
            };

            return noiseFilter.Evaluate(position, evaluator, filterSettings);
        }

        private static float GenerateGas(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter)
        {
            return 0;
        }
    }
}