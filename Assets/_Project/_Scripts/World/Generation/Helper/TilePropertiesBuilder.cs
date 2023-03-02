using System.Collections.Generic;
using Assets._Project._Scripts.World.Data.Enums;
using Assets._Project._Scripts.World.Data.Structs;
using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.ECS.Components;
using Unity.Entities;

namespace Assets._Project._Scripts.World.Generation.Helper
{
    public class TilePropertiesBuilder
    {
        public static TilePropertiesComponentData Build(Entity tile, Dictionary<VegetationZones, float> vegetationZoneHeights)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

            TileAspect tileAspect = entityManager.GetAspect<TileAspect>(tile);
            float height = tileAspect.Position.y;

            TilePropertiesComponentData data = new TilePropertiesComponentData();

            data.VegetationZone = VegetationZoneInterpolator.GetVegetationZoneByHeight(height, vegetationZoneHeights);

            data.TerrainValues = new TerrainValues();
            data.FloraValues = new FloraValues();
            data.FaunaValues = new FaunaValues();
            data.PopulationValues = new PopulationValues();

            return data;
        }
    }
}