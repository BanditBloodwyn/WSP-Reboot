using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    [BurstCompile]
    public partial struct GetTileValuesJob : IJobEntity
    {
        public TileProperties Property;
        public NativeArray<TileValue> TileValues;

        [BurstCompile]
        public void Execute([EntityIndexInQuery] int entityIndex, in LocalTransform localTransform, in TilePropertiesComponentData properties)
        {
            float value = GetPropertyValue(properties, Property);

            TileValues[entityIndex] = new TileValue
            {
                X = (int)localTransform.Position.x,
                Z = (int)localTransform.Position.z,
                Value = value
            };
        }

        [BurstCompile]
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