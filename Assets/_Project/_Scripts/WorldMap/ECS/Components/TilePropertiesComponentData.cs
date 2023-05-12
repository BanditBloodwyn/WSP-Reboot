using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs.ComponentData;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.ECS.Components
{
    public struct TilePropertiesComponentData : IComponentData
    {
        public VegetationZones VegetationZone;

        public int X;
        public int Z;

        public TerrainValues TerrainValues;
        public ResourceValues ResourceValues;
        public FloraValues FloraValues;
        public FaunaValues FaunaValues;
        public PopulationValues PopulationValues;
    }
}