using Assets._Project._Scripts.WorldMap.Data.Enums;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.ECS.Systems
{
    public struct GetTileValuesBufferElement : IBufferElementData
    {
        public TileProperties Property;
    }
}