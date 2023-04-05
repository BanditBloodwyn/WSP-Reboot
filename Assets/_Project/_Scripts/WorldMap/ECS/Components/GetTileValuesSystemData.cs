using Assets._Project._Scripts.WorldMap.Data.Structs;
using Unity.Collections;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.ECS.Components
{
    public struct GetTileValuesSystemData : IComponentData
    {
        public bool Updated;
        public NativeArray<TileValue> TileValues;
    }
}