using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.Data.Structs.ComponentData;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.TileData
{
    public static class TileDataGenerator
    {
        public static TilePropertiesComponentData Generate(
            Entity tile,
            WorldCreationParameters settings)
        {
            // prepare
            EmptyTileAspect tileAspect = tile.GetEmptyTileAspect();
            float height = tileAspect.Position.y;

            TilePropertiesComponentData data = new TilePropertiesComponentData();

            data.TerrainValues.Height = height;
            data.X = (int)tileAspect.Position.x;
            data.Z = (int)tileAspect.Position.z;

            int size = settings.WorldSize.TileAmountPerAxis * settings.WorldSize.ChunkCountPerAxis;

            // build
            data.VegetationZone = GetVegetationZoneByHeight(height, settings);

            data.FloraValues = FloraGenerator.Generate(data, tileAspect.Position, settings);
            data.FaunaValues = FaunaGenerator.Generate(data, tileAspect.Position, settings);
            data.ResourceValues = ResourceGenerator.Generate(data, tileAspect.Position, settings);
            data.PopulationValues = new PopulationValues();

            return data;
        }

        private static VegetationZones GetVegetationZoneByHeight(float height, WorldCreationParameters settings)
        {
            foreach (VegetationZoneHeight zone in settings.VegetationZoneSettings.VegetationZones)
            {
                if (height > zone.MaximumHeight)
                    continue;
                return zone.VegetationZone;
            }

            return VegetationZones.Water;
        }

    }
}