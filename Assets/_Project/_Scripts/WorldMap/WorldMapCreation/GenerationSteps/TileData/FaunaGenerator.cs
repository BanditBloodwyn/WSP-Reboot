using System.Linq;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.TileData
{
    public static class FaunaGenerator
    {
        public static FaunaValues Generate(
            TilePropertiesComponentData data,
            float3 position,
            WorldCreationParameters settings)
        {
            if (data.VegetationZone is VegetationZones.Water)
                return default;

            FaunaValues faunaValues = new();
            faunaValues.Herbivores = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Herbivores");
            faunaValues.Carnivores = GenerateResource(position, settings.ResourceSettings.ResourceProperties, "Carnivores");

            return faunaValues;
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