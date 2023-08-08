using Assets._Project._Scripts.Core.Data.ECS;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Jobs;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.Helpers
{
    public static class TileValuesExtractor
    {
        public static TileValue[] GetChunkTileValues(TileProperties property)
        {
            TilePropertiesComponentData[] tileData = ComponentAccessor.GetAllComponents<TilePropertiesComponentData>();

            NativeArray<TilePropertiesComponentData> tileComponentArray = new NativeArray<TilePropertiesComponentData>(
                tileData,
                Allocator.TempJob);
            NativeArray<TileValue> tileValuesArray = CollectionHelper.CreateNativeArray<TileValue>(
                tileData.Length,
                World.DefaultGameObjectInjectionWorld.UpdateAllocator.ToAllocator,
                NativeArrayOptions.UninitializedMemory);

            GetTileValuesJob job = new GetTileValuesJob();
            job.Property = property;
            job.TilePropertiesComponents = tileComponentArray;
            job.TileValues = tileValuesArray;

            JobHandle jobHandle = job.Schedule(tileData.Length, 12);
            jobHandle.Complete();

            TileValue[] tileValues = job.TileValues.ToArray();

            tileComponentArray.Dispose();
            tileValuesArray.Dispose();

            return tileValues;
        }
    }
}