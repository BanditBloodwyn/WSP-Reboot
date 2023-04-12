using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Collections;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    public struct GetTileValuesJob2 : IJobParallelFor
    {
        public TileProperties Property;

        [ReadOnly] public NativeArray<TileAspect> TileAspects;
        public NativeArray<TileValue> TileValues;

        public void Execute(int index)
        {
            TileAspect tileAspect = TileAspects[index];
            TilePropertiesComponentData data = tileAspect.GetData();

            float value = GetPropertyValue(data, Property);
            TileValues[index] = new TileValue
            {
                X = (int)tileAspect.Position.x,
                Z = (int)tileAspect.Position.z,
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