using Assets._Project._Scripts.Core.Data.ECS;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement.WorldInterfaceHelper
{
    public static class TileFinder
    {
        internal static bool FindClosestOfTiles(
            Entity[] tiles,
            float3 searchPosition,
            out TileAspect? foundTile,
            out float tileDistanceFromSearchPosition)
        {
            tileDistanceFromSearchPosition = float.MaxValue;
            foundTile = null;

            foreach (Entity entity in tiles)
            {
                float3 entityPosition = ComponentAccessor.GetComponentOfEntity<LocalTransform>(entity).Position;
                float distance = math.distance(searchPosition, entityPosition);

                if (!(distance < tileDistanceFromSearchPosition))
                    continue;

                tileDistanceFromSearchPosition = distance;
                foundTile = AspectAccessor.GetAspectOfEntity<TileAspect>(entity);
            }

            return foundTile != null;
        }

        public static EmptyTileAspect[] GetAllEmtpyTilesFromChunkId(params long[] chunkIds)
        {
            List<EmptyTileAspect> tileAspects = new();

            foreach (EmptyTileAspect aspect in AspectAccessor.GetAspectsOfEntitiesWithComponents<EmptyTileAspect>(typeof(ChunkAssignmentComponentData)))
            {
                if (chunkIds.Contains(aspect.ChunkID))
                    tileAspects.Add(aspect);
            }

            return tileAspects.ToArray();
        }
    }
}