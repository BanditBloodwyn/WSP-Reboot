using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using ChunkComponent = Assets._Project._Scripts.WorldMap.WorldMapCore.Types.ChunkComponent;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement.WorldInterfaceHelper
{
    public static class TileFinder
    {
        internal static bool FindNearestTileOfChunk(
            ChunkComponent chunk,
            float3 searchPosition,
            out TileAspect? foundTile,
            out float tileDistanceFromSearchPosition)
        {
            tileDistanceFromSearchPosition = float.MaxValue;
            foundTile = null;

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            foreach (Entity entity in chunk.Tiles)
            {
                if (!entityManager.Exists(entity))
                    continue;

                float3 entityPosition = entityManager.GetComponentData<LocalTransform>(entity).Position;
                float distance = math.distance(searchPosition, entityPosition);

                if (!(distance < tileDistanceFromSearchPosition))
                    continue;

                tileDistanceFromSearchPosition = distance;
                foundTile = entityManager.GetAspect<TileAspect>(entity);
            }

            return foundTile != null;
        }

        public static EmptyTileAspect[] GetAllEmtpyTilesFromChunkId(int chunkId)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;


            EntityQuery tileQuery = entityManager.CreateEntityQuery(typeof(ChunkAssignmentComponentData));

            NativeArray<Entity> tiles = tileQuery.ToEntityArray(Allocator.Temp);

            List<EmptyTileAspect> tileAspects = new();
            
            foreach (Entity entity in tiles)
            {
                EmptyTileAspect aspect = entityManager.GetAspect<EmptyTileAspect>(entity);
                if(aspect.ChunkID == chunkId) 
                    tileAspects.Add(aspect);
            }

            return tileAspects.ToArray();
        }
    }
}