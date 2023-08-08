using Assets._Project._Scripts.Core.Data.ECS;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Helpers;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapManagement.WorldInterfaceHelper;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement
{
    public class WorldInterface
    {
        #region Singleton

        private static WorldInterface instance;

        private WorldInterface() { }

        public static WorldInterface Instance => instance ??= new WorldInterface();

        #endregion


        public List<Chunk> Chunks = new();

        public bool TryGetTileFromChunkAndPosition(ChunkComponent chunk, float3 position, out TileAspect? tile)
        {
            return TileFinder.FindClosestOfTiles(chunk.Tiles, position, out tile, out _);
        }

        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            return TileValuesExtractor.GetChunkTileValues(property);
        }

        public WorldMapCore.ECS.Components.ChunkComponent[] GetAllChunks()
        {
            return ComponentAccessor.GetAllComponents<WorldMapCore.ECS.Components.ChunkComponent>();
        }

        public EmptyTileAspect[] GetAllEmtpyTilesFromChunkId(params long[] chunkIds)
        {
            return TileFinder.GetAllEmtpyTilesFromChunkId(chunkIds);
        }
    }
}