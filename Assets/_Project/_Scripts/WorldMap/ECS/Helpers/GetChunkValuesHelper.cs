using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Jobs;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.ECS.Helpers
{
    public class GetChunkValuesHelper
    {
        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            Entity[] tiles = Landscape.Instance.Chunks
                .SelectMany(static chunk => chunk.Tiles)
                .ToArray();

            NativeArray<TileAspect> tileAspectArray = GetChunkTileAspects(tiles);
            NativeArray<TileValue> tileValuesArray = new NativeArray<TileValue>(tiles.Length, Allocator.Persistent);

            GetTileValuesJob job = new GetTileValuesJob();
            job.Property = property;
            job.TileAspects = tileAspectArray;
            job.TileValues = tileValuesArray;

            JobHandle jobHandle = job.Schedule(tiles.Length, 12);
            jobHandle.Complete();

            TileValue[] tileValues = job.TileValues.ToArray();

            tileAspectArray.Dispose();
            tileValuesArray.Dispose();

            return tileValues.ToArray();
        }

        private NativeArray<TileAspect> GetChunkTileAspects(Entity[] tiles)
        {
            NativeList<TileAspect> tileAspectList = new NativeList<TileAspect>(tiles.Length, Allocator.Persistent);
            
            foreach (Entity tile in tiles)
                tileAspectList.Add(World.DefaultGameObjectInjectionWorld.EntityManager.GetAspect<TileAspect>(tile));
            
            NativeArray<TileAspect> tileAspectArray = tileAspectList.ToArray(Allocator.Persistent);
           
            tileAspectList.Dispose();
            
            return tileAspectArray;
        }
    }
}