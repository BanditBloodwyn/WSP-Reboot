using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Helpers;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement
{
    public class WorldInterface
    {
        #region Singleton

        private static WorldInterface instance;

        private WorldInterface() { }

        public static WorldInterface Instance => instance ??= new WorldInterface();

        #endregion

        private readonly GetChunkValuesHelper _getChunkValuesHelper = new();

        public List<Chunk> Chunks = new();
        public List<ChunkComponent> ChunkComponents = new();

        public bool TryGetTileFromChunkAndPosition(ChunkComponent chunk, float3 position, out TileAspect tile)
        {
            FindNearestTileOfChunk(chunk, position, out tile, out _);
            return true;
        }

        public bool TryGetTileFromPosition(float3 position, out TileAspect tile)
        {
            float nearestDistance = float.MaxValue;
            tile = new TileAspect();

            foreach (ChunkComponent chunk in ChunkComponents)
            {
                FindNearestTileOfChunk(chunk, position, out TileAspect foundTile, out float distance);

                if (!(distance < nearestDistance))
                    continue;

                nearestDistance = distance;
                tile = foundTile;
            }

            return true;
        }

        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            return _getChunkValuesHelper.GetChunkTileValues(property, Chunks);
        }

        private static void FindNearestTileOfChunk(
            ChunkComponent chunk,
            float3 searchCenter,
            out TileAspect tile,
            out float tileDistance)
        {
            tileDistance = float.MaxValue;
            tile = new TileAspect();

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            foreach (Entity entity in chunk.Tiles)
            {
                if (!entityManager.Exists(entity))
                    continue;

                float3 entityPosition = entityManager.GetComponentData<LocalTransform>(entity).Position;
                float distance = math.distance(searchCenter, entityPosition);

                if (!(distance < tileDistance))
                    continue;

                tileDistance = distance;
                tile = entityManager.GetAspect<TileAspect>(entity);
            }
        }
    }
}