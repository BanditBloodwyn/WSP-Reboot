using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components
{
    public struct ChunkComponent : IComponentData
    {
        public int ID;
        public int TileAmountPerAxis;
        public Vector2Int Coordinates;
    }
}