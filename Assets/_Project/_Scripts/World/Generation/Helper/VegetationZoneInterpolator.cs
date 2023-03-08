using Assets._Project._Scripts.World.Data.Enums;
using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.World.Generation.old;

namespace Assets._Project._Scripts.World.Generation.Helper
{
    public static class VegetationZoneInterpolator
    {
        public static Dictionary<VegetationZones, float> GetVegetationZoneHeights(
            WorldCreationParameters parameters,
            float maximumHeight)
        {
            Dictionary<VegetationZones, float> dictionary = new Dictionary<VegetationZones, float>
            {
                { VegetationZones.Water, GetInterpolationByZone(VegetationZones.Water, parameters, maximumHeight) },
                { VegetationZones.Kolline, GetInterpolationByZone(VegetationZones.Kolline, parameters, maximumHeight) },
                { VegetationZones.Montane, GetInterpolationByZone(VegetationZones.Montane, parameters, maximumHeight) },
                { VegetationZones.Subalpine, GetInterpolationByZone(VegetationZones.Subalpine, parameters, maximumHeight) },
                { VegetationZones.Alpine_Trees, GetInterpolationByZone(VegetationZones.Alpine_Trees, parameters, maximumHeight) },
                { VegetationZones.Alpine_Bushes, GetInterpolationByZone(VegetationZones.Alpine_Bushes, parameters, maximumHeight) },
                { VegetationZones.Subnivale, GetInterpolationByZone(VegetationZones.Subnivale, parameters, maximumHeight) },
                { VegetationZones.Nivale, GetInterpolationByZone(VegetationZones.Nivale, parameters, maximumHeight) }
            };

            return dictionary;
        }

        private static float GetInterpolationByZone(VegetationZones vegetationZone, WorldCreationParameters parameters, float maximumHeight)
        {
            return GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(height => height.VegetationZone == vegetationZone)
                    .MaximumHeight / 100,
                maximumHeight) * 100;
        }

        private static float GetInterpolation(
            float height,
            float maximumHeight)
        {
            float interpolation = maximumHeight * height / 100;
            return interpolation;
        }

        public static VegetationZones GetVegetationZoneByHeight(float height, Dictionary<VegetationZones, float> heights)
        {
            foreach (KeyValuePair<VegetationZones, float> zone in heights)
            {
                if(height > zone.Value)
                    continue;
                return zone.Key;
            }

            return VegetationZones.Water;
        }
    }
}