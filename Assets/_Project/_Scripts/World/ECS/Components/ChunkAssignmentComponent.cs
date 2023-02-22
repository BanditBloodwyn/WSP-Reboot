using Unity.Entities;

namespace Assets._Project._Scripts.World.ECS.Components
{
    public struct ChunkAssignmentComponent : IComponentData
    {
        public long ChunkID;
    }
}