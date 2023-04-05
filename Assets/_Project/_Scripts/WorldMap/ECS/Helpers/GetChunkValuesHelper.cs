using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Jobs;
using System.Collections.Generic;
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
                NativeArray<Entity> tilesArray = new NativeArray<Entity>(chunk.Tiles, Allocator.Persistent);
                NativeArray<TileValue> tileValuesArray = new NativeArray<TileValue>(chunk.Tiles.Length, Allocator.Persistent);

                GetTileValuesJob2 job = new GetTileValuesJob2();
                job.Property = property;
                job.Tiles = tilesArray;
                job.TileValues = tileValuesArray;

                JobHandle jobHandle = job.Schedule(chunk.Tiles.Length, 64);
                jobHandle.Complete();

                tileValues.AddRange(job.TileValues.ToArray());

                tilesArray.Dispose();
                tileValuesArray.Dispose();
            }

            return tileValues.ToArray();
        }
    }
}