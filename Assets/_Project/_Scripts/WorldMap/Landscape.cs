using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.Generation;
using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.WorldMap.ECS.Helpers;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap
{
    public class Landscape
    {
        #region Singleton

        private static Landscape instance;

        private Landscape()
        {
            _getChunkValuesHelper.Init();
        }

        public static Landscape Instance => instance ??= new Landscape();

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
            _getChunkValuesHelper.GetChunkTileValues(property);

            return null;
        }

        private static List<(string, string)> GetTileFields()
        {
            List<(string, string)> fields = new List<(string, string)>();

            foreach (string fieldName in typeof(TerrainValues).GetFields().Select(static field => field.Name))
                fields.Add(new(nameof(TerrainValues), fieldName));
            foreach (string fieldName in typeof(FloraValues).GetFields().Select(static field => field.Name))
                fields.Add(new(nameof(FloraValues), fieldName));
            foreach (string fieldName in typeof(FaunaValues).GetFields().Select(static field => field.Name))
                fields.Add(new(nameof(FaunaValues), fieldName));
            foreach (string fieldName in typeof(ResourceValues).GetFields().Select(static field => field.Name))
                fields.Add(new(nameof(ResourceValues), fieldName));
            foreach (string fieldName in typeof(PopulationValues).GetFields().Select(static field => field.Name))
                fields.Add(new(nameof(PopulationValues), fieldName));

            return fields;
        }
    }
}