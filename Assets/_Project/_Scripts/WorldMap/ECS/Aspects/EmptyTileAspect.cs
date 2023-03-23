using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.ECS.Aspects
{
    public readonly partial struct EmptyTileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<ChunkAssignmentComponentData> _chunkAssignment;

        public float3 Position => _transformAspect.LocalPosition;
    }
}