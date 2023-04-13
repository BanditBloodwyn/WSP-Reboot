using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    [BurstCompile]
    public struct GetTileValuesJob : IJobParallelFor
    {
        public TileProperties Property;

        [ReadOnly] public NativeArray<TileAspect> TileAspects;
        public NativeArray<TileValue> TileValues;

        [BurstCompile]
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

        [BurstCompile]
        private static float GetPropertyValue(in TilePropertiesComponentData properties, TileProperties property)
        {
            return property switch
            {
                TileProperties.None => 0,
                TileProperties.Height => properties.TerrainValues.Height,
                TileProperties.DedicuousTrees => properties.FloraValues.DeciduousTrees,
                TileProperties.EvergreenTrees => properties.FloraValues.EvergreenTrees,
                TileProperties.Vegetables => properties.FloraValues.Vegetables,
                TileProperties.Fruits => properties.FloraValues.Fruits,
                TileProperties.Carnivores => properties.FaunaValues.Carnivores,
                TileProperties.Herbivores => properties.FaunaValues.Herbivores,
                TileProperties.Coal => properties.ResourceValues.Coal,
                TileProperties.IronOre => properties.ResourceValues.IronOre,
                TileProperties.GoldOre => properties.ResourceValues.GoldOre,
                TileProperties.Oil => properties.ResourceValues.Oil,
                TileProperties.Gas => properties.ResourceValues.Gas,
                TileProperties.LifeStandard => properties.PopulationValues.LifeStandard,
                TileProperties.Urbanization => properties.PopulationValues.Urbanization,
                _ => 0
            };
        }
    }
}