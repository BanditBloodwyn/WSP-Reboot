using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
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

            int size = settings.TileAmountPerAxis * settings.ChunkCountPerAxis;

            // build
            data.VegetationZone = GetVegetationZoneByHeight(height);

            //data.FloraValues = _floraGenerator.Generate(data, tileAspect.Position);
            //data.FaunaValues = _faunaGenerator.Generate(data);
            //data.ResourceValues = _resourceGenerator.Generate(data, tileAspect.Position, size);
            data.PopulationValues = new PopulationValues();

            return data;
        }

        private static VegetationZones GetVegetationZoneByHeight(float height)
        {
            //foreach (VegetationZoneHeight zone in _vegetationZoneSettings.VegetationZones)
            //{
            //    if (height > zone.MaximumHeight)
            //        continue;
            //    return zone.VegetationZone;
            //}

            return VegetationZones.Water;
        }

    }
}