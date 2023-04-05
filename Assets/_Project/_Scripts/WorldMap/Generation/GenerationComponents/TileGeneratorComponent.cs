using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents
{
    public class TileGeneratorComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;
       
        public bool Enabled => _enabled;

        public void Apply(Chunk chunk)
        {
            List<Entity> entities = new();

            for (int x = 0; x < chunk.Size; x++)
            {
                for (int y = 0; y < chunk.Size; y++)
                {
                    Entity tile = CreateTile(
                        x + chunk.Size * chunk.Coordinates.x - chunk.Size / 2,
                        y + chunk.Size * chunk.Coordinates.y - chunk.Size / 2,
                        chunk.Coordinates.y * chunk.Size + chunk.Coordinates.x);

                    entities.Add(tile);
                }
            }

            chunk.Tiles = entities.ToArray();
        }

        private static Entity CreateTile(int xPos, int yPos, int chunkID)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity tile = entityManager.CreateEntity();

            entityManager.AddComponent<LocalTransform>(tile);
            entityManager.SetComponentData(tile, new LocalTransform { Position = new float3(xPos, 0, yPos) });

            entityManager.AddComponent<ChunkAssignmentComponentData>(tile);
            entityManager.SetComponentData(tile, new ChunkAssignmentComponentData { ChunkID = chunkID });
            return tile;
        }
    }
}