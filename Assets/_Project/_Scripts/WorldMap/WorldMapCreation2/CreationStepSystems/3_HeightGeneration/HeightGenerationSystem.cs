using Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._2_TileCreation;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents;
using System.Diagnostics;
using Unity.Entities;
using Unity.Jobs;
using Debug = UnityEngine.Debug;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._3_HeightGeneration
{
    [UpdateAfter(typeof(TileCreationSystem))]
    public partial class HeightGenerationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<WorldCreationParametersComponent>();
        }

        protected override void OnUpdate()
        {
            Enabled = false;

            Stopwatch stopwatch = Stopwatch.StartNew();

            WorldCreationParametersComponent settings = SystemAPI.GetSingleton<WorldCreationParametersComponent>();
            
            JobHandle job = default;
            job = new HeightGenerationJob
            {
                Settings = settings
            }.ScheduleParallel(job);
            job.Complete();

            stopwatch.Stop();
            Debug.Log($"Process time <b><i>HeightGenerationSystem</i></b>: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}