using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    public partial struct GetTileAspectsJob : IJobChunk
    {
        public NativeArray<TileAspect> TileAspects;
        public ComponentTypeHandle<TileAspect> TileAspectTypeHandle;

        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {

        }
    }
}