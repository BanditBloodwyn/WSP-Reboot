using Assets._Project._Scripts.World.Data.Enums;
using Assets._Project._Scripts.World.Data.Structs;
using Unity.Entities;

namespace Assets._Project._Scripts.World.ECS.Components
{
    public struct TilePropertiesComponentData : IComponentData
    {
        public VegetationZones VegetationZone;

        public TerrainValues TerrainValues;
        public FloraValues FloraValues;
        public FaunaValues FaunaValues;
        public PopulationValues PopulationValues;
    }
}