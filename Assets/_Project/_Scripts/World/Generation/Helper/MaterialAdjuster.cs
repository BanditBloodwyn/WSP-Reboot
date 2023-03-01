using Assets._Project._Scripts.World.Data;
using System.Linq;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.Helper
{
    public static class MaterialAdjuster
    {
        public static void AdjustMaterialSettings(
            WorldCreationParameters parameters,
            Material tileMaterial,
            float maximumHeight,
            float minimumHeight)
        {
            float waterInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Water)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float kollineInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Kolline)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float montaneInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Montane)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float subalpineInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Subalpine)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float alpine_TreesInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Alpine_Trees)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float alpine_BushesInterpolation = GetInterpolation(parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Alpine_Bushes)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float subnivaleInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Subnivale)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;
            float nivaleInterpolation = GetInterpolation(
                parameters.vegetationZoneHeights
                    .First(static height => height.VegetationZone == VegetationZones.Nivale)
                    .MaximumHeight / 100,
                maximumHeight,
                minimumHeight) * 100;

            tileMaterial.SetFloat("_WaterHeight", waterInterpolation);
            tileMaterial.SetFloat("_KollineHeight", kollineInterpolation);
            tileMaterial.SetFloat("_MontaneHeight", montaneInterpolation);
            tileMaterial.SetFloat("_SubalpineHeight", subalpineInterpolation);
            tileMaterial.SetFloat("_Alpine_TreesHeight", alpine_TreesInterpolation);
            tileMaterial.SetFloat("_Alpine_BushesHeight", alpine_BushesInterpolation);
            tileMaterial.SetFloat("_SubnivaleHeight", subnivaleInterpolation);
            tileMaterial.SetFloat("_NivaleHeight", nivaleInterpolation);
        }

        public static float GetInterpolation(
            float height,
            float maximumHeight,
            float minimumHeight)
        {
            //float interpolation = 100 / (maximumHeight - minimumHeight) * (height - minimumHeight) / 100;
            float interpolation = maximumHeight * height / 100;
            return interpolation;
        }
    }
}