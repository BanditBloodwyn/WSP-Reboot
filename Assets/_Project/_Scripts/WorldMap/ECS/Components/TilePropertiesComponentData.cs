using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.ECS.Components
{
    public struct TilePropertiesComponentData : IComponentData
    {
        public VegetationZones VegetationZone;

        public TerrainValues TerrainValues;
        public ResourceValues ResourceValues;
        public FloraValues FloraValues;
        public FaunaValues FaunaValues;
        public PopulationValues PopulationValues;
    }
}