using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using TilePropertiesComponentData = Assets._Project._Scripts.WorldMap.ECS.Components.TilePropertiesComponentData;

namespace Assets._Project._Scripts.WorldMap.ECS.Systems
{
    [BurstCompile]
    public partial struct GetTileValuesSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TilePropertiesComponentData>();

            AddDataComponent(ref state);
            AddBuffer(ref state);
        }

        [BurstCompile]
        private void AddDataComponent(ref SystemState state)
        {
            state.EntityManager.AddComponent<GetTileValuesSystemData>(state.SystemHandle);

            NativeArray<TileValue> nativeArray = new NativeArray<TileValue>(
                state.EntityManager.CreateEntityQuery(typeof(TilePropertiesComponentData)).CalculateEntityCount(),
                Allocator.Persistent);

            GetTileValuesSystemData systemData = new GetTileValuesSystemData
            {
                TileValues = nativeArray
            };
            SystemAPI.SetComponent(state.SystemHandle, systemData);
        }

        [BurstCompile]
        private static void AddBuffer(ref SystemState state)
        {
            Entity bufferEntity = state.EntityManager.CreateEntity();
            state.EntityManager.AddBuffer<GetTileValuesBufferElement>(bufferEntity);
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (DynamicBuffer<GetTileValuesBufferElement> dynamicBuffer in SystemAPI.Query<DynamicBuffer<GetTileValuesBufferElement>>())
            {
                foreach (GetTileValuesBufferElement input in dynamicBuffer)
                    PerformJob(ref state, input.Property);

                dynamicBuffer.Clear();
            }
        }

        [BurstCompile]
        private void PerformJob(ref SystemState state, TileProperties property)
        {
            NativeArray<TileValue> tileValues = CollectionHelper.CreateNativeArray<TileValue>(
                state.EntityManager.CreateEntityQuery(typeof(TilePropertiesComponentData)).CalculateEntityCount(),
                Allocator.TempJob);
            
            GetTileValuesJob job = new GetTileValuesJob
            {
                TileValues = tileValues,
                Property = property
            };

            Debug.Log($"Schedule GetTileValuesJob - Tiles: {tileValues.Length}");

            job.ScheduleParallel<GetTileValuesJob>(state.Dependency).Complete();

            state.EntityManager.SetComponentData(state.SystemHandle, new GetTileValuesSystemData
            {
                TileValues = tileValues
            });

            tileValues.Dispose();
        }
    }
}