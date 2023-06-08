using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components
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