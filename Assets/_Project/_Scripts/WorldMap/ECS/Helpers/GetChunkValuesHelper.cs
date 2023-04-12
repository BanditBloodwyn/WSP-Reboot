using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Jobs;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.ECS.Helpers
{
    public class GetChunkValuesHelper
    {
        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            List<TileValue> tileValues = new List<TileValue>();

            foreach (Chunk chunk in Landscape.Instance.Chunks)
            {
                NativeList<TileAspect> tileAspectList = new NativeList<TileAspect>(chunk.Tiles.Length, Allocator.Persistent);
                foreach (Entity tile in chunk.Tiles)
                    tileAspectList.Add(World.DefaultGameObjectInjectionWorld.EntityManager.GetAspect<TileAspect>(tile));
                NativeArray<TileAspect> tileAspectArray = tileAspectList.ToArray(Allocator.Persistent);

                NativeArray<TileValue> tileValuesArray = new NativeArray<TileValue>(chunk.Tiles.Length, Allocator.Persistent);

                GetTileValuesJob job = new GetTileValuesJob();
                job.Property = property;
                job.TileAspects = tileAspectArray;
                job.TileValues = tileValuesArray;

                JobHandle jobHandle = job.Schedule(chunk.Tiles.Length, 64);
                jobHandle.Complete();

                tileValues.AddRange(job.TileValues.ToArray());

                tileAspectArray.Dispose();
                tileAspectList.Dispose();
                tileValuesArray.Dispose();
            }

            return tileValues.ToArray();
        }
    }
}