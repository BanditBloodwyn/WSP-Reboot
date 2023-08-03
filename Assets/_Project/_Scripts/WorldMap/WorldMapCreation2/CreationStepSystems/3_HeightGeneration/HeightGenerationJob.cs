using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._3_HeightGeneration.HeightCalculation;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._3_HeightGeneration
{
    [BurstCompile]
    public partial struct HeightGenerationJob : IJobEntity
    {
        public HeightCalculator HeightCalculator;

        [BurstCompile]
        private void Execute([ChunkIndexInQuery] int chunkIndex, ref LocalTransform tileTransform, in ChunkAssignmentComponentData chunkAssignment)
        {
            float noiseValue = HeightCalculator.Evaluate(tileTransform.Position.x, tileTransform.Position.z);

            LocalTransform newLocalTransform = new LocalTransform();
            newLocalTransform.Position = new float3(
                tileTransform.Position.x,
                noiseValue,
                tileTransform.Position.z);

            tileTransform = newLocalTransform;
        }
    }
}