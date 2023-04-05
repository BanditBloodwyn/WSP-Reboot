using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    public struct GetTileValuesJob2 : IJobParallelFor
    {
        public TileProperties Property;
        [ReadOnly] public NativeArray<Entity> Tiles;
        public NativeArray<TileValue> TileValues;

        public void Execute(int index)
        {
            TileAspect aspect = World.DefaultGameObjectInjectionWorld.EntityManager.GetAspect<TileAspect>(Tiles[index]);
            TilePropertiesComponentData data = aspect.GetData();

            float value = GetPropertyValue(data, Property);
            TileValues[index] = new TileValue
            {
                X = (int)aspect.Position.x,
                Z = (int)aspect.Position.z,
                Value = value
            };
        }

        private static float GetPropertyValue(in TilePropertiesComponentData properties, TileProperties property)
        {
            return property switch
            {
                TileProperties.Height => properties.TerrainValues.Height,
                TileProperties.DedicuousTrees => properties.FloraValues.DeciduousTrees,
                TileProperties.EvergreenTrees => properties.FloraValues.EvergreenTrees,
                TileProperties.Vegetables => properties.FloraValues.Vegetables,
                TileProperties.Fruits => properties.FloraValues.Fruits,
                _ => 0
            };
        }
    }
}