using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    [BurstCompile]
    public partial struct GetTileValuesJob2 : IJobParallelFor
    {
        public TileProperties Property;

        [ReadOnly] public NativeArray<TilePropertiesComponentData> TilePropertiesComponents;
        public NativeArray<TileValue> TileValues;

        [BurstCompile]
        public void Execute(int index)
        {
            TilePropertiesComponentData data = TilePropertiesComponents[index];

            float value = GetPropertyValue(data, Property);
            TileValues[index] = new TileValue
            {
                X = data.X, 
                Z = data.Z,
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