using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Helpers;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

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

        public bool TryGetTileFromChunkAndPosition(ChunkComponent chunk, Vector3 position, out TileAspect tile)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            float nearestDistance = float.MaxValue;
            tile = new TileAspect();

            foreach (Entity entity in chunk.Tiles)
            {
                if (entityManager.Exists(entity))
                {
                    float3 entityPosition = entityManager.GetComponentData<LocalTransform>(entity).Position;
                    float distance = math.distance(position, entityPosition);

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        tile = entityManager.GetAspect<TileAspect>(entity);
                    }
                }
            }

            return true;
        }

        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            return _getChunkValuesHelper.GetChunkTileValues(property, Chunks);
        }
    }
}