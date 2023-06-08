using System.Linq;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.TileData
{
    public static class ResourceGenerator
    {
        public static ResourceValues Generate(
            TilePropertiesComponentData data,
            float3 position,
            WorldCreationParameters settings)
        {
            ResourceValues resourceValues = new ResourceValues();
            resourceValues.Coal = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Coal");
            resourceValues.IronOre = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Iron");
            resourceValues.GoldOre = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Gold");
            resourceValues.Oil = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Oil");
            resourceValues.Gas = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Gas");

            return resourceValues;
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