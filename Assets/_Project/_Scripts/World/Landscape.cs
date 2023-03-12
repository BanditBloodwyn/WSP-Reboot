using System.Collections.Generic;
using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.Generation;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets._Project._Scripts.World
{
    public class Landscape
    {
        #region Singleton

        private static Landscape instance;

        private Landscape() { }

        public static Landscape Instance => instance ??= new Landscape();

        #endregion

        public List<Chunk> Chunks = new();

        public bool TryGetTileFromChunkAndPosition(ChunkComponent chunk, Vector3 position, out TileAspect tile)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

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

    }
}