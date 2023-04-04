using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.Core.Reflection;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Generation;
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

        private Landscape() { }

        public static Landscape Instance => instance ??= new Landscape();

        #endregion

        private readonly List<(string, string)> TileFields = GetTileFields();

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

        public Dictionary<Tuple<int, int>, float> GetChunkTileValues(string propertyName)
        {
            if (TileFields.All(field => field.Item2 != propertyName))
                return null;

            Dictionary<Tuple<int, int>, float> values = new();
            (string container, string valueName) = TileFields.First(field => field.Item2 == propertyName);

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            foreach (Chunk chunk in Chunks)
            {
                foreach (Entity tile in chunk.Tiles)
                {
                    TileAspect aspect = entityManager.GetAspect<TileAspect>(tile);
                    TilePropertiesComponentData data = aspect.GetData();

                    float value = ReflectionHelper.GetFieldValueFromStruct(data, container, valueName);
                    values.Add(new Tuple<int, int>((int)aspect.Position.x, (int)aspect.Position.z), value);
                }
            }
            return values;
        }

        private static List<(string, string)> GetTileFields()
        {
            List<(string, string)> fields = new List<(string, string)>();

            foreach (string fieldName in typeof(TerrainValues).GetFields().Select(static field => field.Name)) 
                fields.Add(new (nameof(TerrainValues), fieldName));
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