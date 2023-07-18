using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents;
using System.Diagnostics;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using ChunkComponent = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components.ChunkComponent;
using Debug = UnityEngine.Debug;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._1_ChunkCreation
{
    public partial class ChunkCreationSystem : SystemBase
    {
        protected override void OnCreate()
        {
            RequireForUpdate<WorldCreationControlComponent>();
        }

        protected override void OnUpdate()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            CleanupSystemTriggers();
            CreateChunks();

            stopwatch.Stop();
            Debug.Log($"Process time <b><i>ChunkCreationSystem</i></b>: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void CreateChunks()
        {
            EntityArchetype chunkArchetype = EntityManager.CreateArchetype(
                typeof(ChunkComponent));

            WorldCreationParametersComponent settings = SystemAPI.GetSingleton<WorldCreationParametersComponent>();

            for (int x = 0; x < settings.WorldSize.ChunkCountPerAxis; x++)
            for (int y = 0; y < settings.WorldSize.ChunkCountPerAxis; y++)
                CreateChunkEntity(chunkArchetype, x, y, settings);
        }

        private void CreateChunkEntity(EntityArchetype chunkArchetype, int x, int y, WorldCreationParametersComponent settings)
        {
            Entity chunkEntity = EntityManager.CreateEntity(chunkArchetype);
            EntityManager.SetName(chunkEntity, $"Chunk_{x},{y}");

            ChunkComponent chunkComponent = new()
            {
                ID = y * settings.WorldSize.ChunkCountPerAxis + x,
                TileAmountPerAxis = settings.WorldSize.TileAmountPerAxis,
                Coordinates = new Vector2Int(x, y)
            };
            SystemAPI.SetComponent(chunkEntity, chunkComponent);
        }

        private void CleanupSystemTriggers()
        {
            NativeArray<Entity> array = SystemAPI.QueryBuilder()
                .WithAll<WorldCreationControlComponent>()
                .Build()
                .ToEntityArray(Allocator.Temp);

            foreach (Entity entity in array)
                EntityManager.RemoveComponent<WorldCreationControlComponent>(entity);

            array.Dispose();
        }
    }
}