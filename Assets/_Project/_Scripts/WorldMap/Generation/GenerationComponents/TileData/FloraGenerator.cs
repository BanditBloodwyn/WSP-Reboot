using Assets._Project._Scripts.Core.Math;
using Assets._Project._Scripts.Core.Math.Noise;
using Assets._Project._Scripts.Core.Math.Noise.NoiseFilters;
using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.TileData
{
    [CreateAssetMenu(fileName = "FloraGenerator", menuName = "ScriptableObjects/Settings/World/Generators/Flora Generator")]
    public class FloraGenerator : ScriptableObject
    {
        [SerializeField] private WorldCreationParameters _worldCreationParameters;

        public FloraValues Generate(
            TilePropertiesComponentData data,
            float3 position)
        {
            if (data.VegetationZone is VegetationZones.Water or VegetationZones.Subnivale or VegetationZones.Nivale)
                return default;

            PerlinNoiseEvaluator evaluator = new PerlinNoiseEvaluator();
            StandardNoiseFilter noiseFilter = new StandardNoiseFilter();

            FloraValues floraValues = new FloraValues();
            floraValues.DeciduousTrees = GenerateDeciduoudTrees(position, evaluator, noiseFilter);
            floraValues.EvergreenTrees = GenerateEvergreenTrees(position, evaluator, noiseFilter, _worldCreationParameters);
            floraValues.Vegetables = GenerateVegetables(position, evaluator, noiseFilter);
            floraValues.Fruits = GenerateFruits(position, evaluator, noiseFilter, _worldCreationParameters);

            return floraValues;
        }

        private static float GenerateDeciduoudTrees(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter)
        {
            DefaultNoiseFilterSettings deciduousTreesFilterSettings = new DefaultNoiseFilterSettings
            {
                NumberOfLayers = 4,
                Strength = 72,
                MinValue = 0,
                MaxValue = 100,
                BaseRoughness = MathFunctions.Bump(position.y, 100, 20, 20, 5)
            };
           
            return noiseFilter.Evaluate(position, evaluator, deciduousTreesFilterSettings);
        }

        private static float GenerateEvergreenTrees(
            float3 position,
            PerlinNoiseEvaluator evaluator,
            StandardNoiseFilter noiseFilter,
            WorldCreationParameters worldCreationParameters)
        {
            DefaultNoiseFilterSettings evergreenTreesFilterSettings = new DefaultNoiseFilterSettings
            {
                NumberOfLayers = 4,
                Strength = 72,
                MinValue = 0,
                MaxValue = 100,
                Center = new Vector3(0, 0, worldCreationParameters.ChunkCountPerAxis / 2f),
                BaseRoughness = MathFunctions.Bump(position.y, 100, 15, 50, 5)
            };

            return noiseFilter.Evaluate(position, evaluator, evergreenTreesFilterSettings);
        }

        private static float GenerateVegetables(
            float3 position, 
            PerlinNoiseEvaluator evaluator, 
            StandardNoiseFilter noiseFilter)
        {
            DefaultNoiseFilterSettings vegetablesFilterSettings = new DefaultNoiseFilterSettings
            {
                NumberOfLayers = 2,
                Strength = 200,
                MinValue = 0.5f,
                MaxValue = 100,
                BaseRoughness = MathFunctions.Bump(position.y, 50, 45, 10, 5)
            };

            return noiseFilter.Evaluate(position, evaluator, vegetablesFilterSettings);
        }
     
        private static float GenerateFruits(
            float3 position, 
            PerlinNoiseEvaluator evaluator, 
            StandardNoiseFilter noiseFilter, 
            WorldCreationParameters worldCreationParameters)
        {
            DefaultNoiseFilterSettings fruitsFilterSettings = new DefaultNoiseFilterSettings
            {
                NumberOfLayers = 2,
                Strength = 200,
                MinValue = 0.5f,
                MaxValue = 100,
                Center = new Vector3(0, worldCreationParameters.ChunkCountPerAxis / 2f, 0),
                BaseRoughness = MathFunctions.Bump(position.y, 30, 25, 10, 5)
            };

            return noiseFilter.Evaluate(position, evaluator, fruitsFilterSettings);
        }
    }
}