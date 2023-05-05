using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Tiles
{
    public static class TileCreator
    {
        public static Entity CreateTile(int xPos, int yPos, int chunkID)
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