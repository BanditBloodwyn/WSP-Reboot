using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._3_HeightGeneration
{
    [BurstCompile]
    public partial struct HeightGenerationJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

        [BurstCompile]
        private void Execute([ChunkIndexInQuery] int chunkIndex, ref LocalTransform tileTransform, in ChunkAssignmentComponentData chunkAssignment)
        {
            float noiseValue = noise.snoise(new float2(tileTransform.Position.x, tileTransform.Position.z));
            if (noiseValue != 0)
                ;

            LocalTransform newLocalTransform = new LocalTransform();
            newLocalTransform.Position = new float3(
                tileTransform.Position.x,
                noiseValue, 
                tileTransform.Position.z);

            tileTransform = newLocalTransform;
        }
    }
}