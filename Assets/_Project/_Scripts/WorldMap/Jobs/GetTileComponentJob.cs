using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    [BurstCompile]
    public partial struct GetTileComponentJob : IJobChunk
    {
        [ReadOnly] public ComponentTypeHandle<TilePropertiesComponentData> ComponentTypeHandle;
        [ReadOnly] public NativeArray<Entity> entities;
        public NativeArray<TilePropertiesComponentData> data;

        [BurstCompile]
        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            NativeArray<TilePropertiesComponentData> dataArray = chunk.GetNativeArray(ref ComponentTypeHandle);

            for (int i = 0; i < dataArray.Length; i++)
                data[unfilteredChunkIndex * 128 + i] = dataArray[i];
        }
    }
}