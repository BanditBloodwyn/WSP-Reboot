using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.Data.Structs.ComponentData;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System.Linq;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.TileData
{
    public static class FloraGenerator
    {
        public static FloraValues Generate(
            TilePropertiesComponentData data,
            float3 position,
            WorldCreationParameters settings)
        {
            if (data.VegetationZone is VegetationZones.Water or VegetationZones.Subnivale or VegetationZones.Nivale)
                return default;

            FloraValues floraValues = new FloraValues();
            floraValues.DeciduousTrees = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Dedicuous Trees");
            floraValues.EvergreenTrees = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Evergreen Trees");
            floraValues.Herbs = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Herbs");
            floraValues.Fruits = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Fruits");

            return floraValues;
        }


        private static float GenerateResource(
            float3 position,
            ResourceProperties[] resourceProperties,
            string name)
        {
            ResourceProperties property = resourceProperties.First(prop => prop.ResourceName == name);

            float value = property.NoiseFilter.Evaluate(position, property.Seed);
            return value * property.Distribution.Evaluate(position.y);
        }
    }
}