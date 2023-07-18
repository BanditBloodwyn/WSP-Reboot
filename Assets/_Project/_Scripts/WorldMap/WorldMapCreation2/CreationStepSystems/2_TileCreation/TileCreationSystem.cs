using Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._1_ChunkCreation;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents;
using System.Diagnostics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Debug = UnityEngine.Debug;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._2_TileCreation
{
    [UpdateAfter(typeof(ChunkCreationSystem))]
    public partial class TileCreationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<WorldCreationParametersComponent>();
        }

        protected override void OnUpdate()
        {
            Enabled = false;

            Stopwatch stopwatch = Stopwatch.StartNew();

            EntityCommandBuffer entityCommandBuffer = new(Allocator.TempJob);

            JobHandle job = default;
            job = new TileCreationJob { EntityCommandBuffer = entityCommandBuffer.AsParallelWriter() }
                .ScheduleParallel(job);
            job.Complete();

            entityCommandBuffer.Playback(EntityManager);
            entityCommandBuffer.Dispose();

            stopwatch.Stop();
            Debug.Log($"Process time <b><i>TileCreationSystem</i></b>: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}