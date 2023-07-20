using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects
{
    public readonly partial struct EmptyTileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;

        private readonly RefRO<ChunkAssignmentComponentData> _chunkAssignment;

        public float3 Position => _localTransform.ValueRO.Position;

        public long ChunkID => _chunkAssignment.ValueRO.ChunkID;
    }
}