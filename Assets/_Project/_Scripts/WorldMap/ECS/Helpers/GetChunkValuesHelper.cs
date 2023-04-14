using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Jobs;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.ECS.Helpers
{
    public class GetChunkValuesHelper
    {
        private JobHandle _jobHandle;

        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            Entity[] tiles = Landscape.Instance.Chunks
                .SelectMany(static chunk => chunk.Tiles)
                .ToArray();

            TilePropertiesComponentData[] tileData = GetChunkTileAspects2(tiles);

            NativeArray<TilePropertiesComponentData> tileComponentArray = new NativeArray<TilePropertiesComponentData>(tileData, Allocator.Persistent);
            NativeArray<TileValue> tileValuesArray = new NativeArray<TileValue>(tiles.Length, Allocator.Persistent);

            GetTileValuesJob2 job = new GetTileValuesJob2();
            job.Property = property;
            job.TilePropertiesComponents = tileComponentArray;
            job.TileValues = tileValuesArray;

            JobHandle jobHandle = job.Schedule(tiles.Length, 12);
            jobHandle.Complete();

            TileValue[] tileValues = job.TileValues.ToArray();

            tileComponentArray.Dispose();
            tileValuesArray.Dispose();

            return tileValues;
        }

        private TilePropertiesComponentData[] GetChunkTileAspects2(Entity[] tiles)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            NativeArray<Entity> tileArray = new NativeArray<Entity>(tiles, Allocator.Persistent);
            NativeArray<TilePropertiesComponentData> tileComponentArray = new NativeArray<TilePropertiesComponentData>(tiles.Length, Allocator.Persistent);
            EntityQuery query = new EntityQueryBuilder(Allocator.Temp)
                .WithAll<TilePropertiesComponentData>()
                .Build(entityManager);

            GetTileComponentJob job = new GetTileComponentJob();
            job.entities = tileArray;
            job.data = tileComponentArray;
            job.ComponentTypeHandle = entityManager.GetComponentTypeHandle<TilePropertiesComponentData>(true);

            _jobHandle = job.Schedule(query, _jobHandle);
            _jobHandle.Complete();

            TilePropertiesComponentData[] tileData = job.data.ToArray();

            tileArray.Dispose();
            tileComponentArray.Dispose();

            return tileData;
        }
    }
}