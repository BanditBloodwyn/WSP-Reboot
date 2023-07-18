using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using ChunkComponent = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components.ChunkComponent;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement.WorldInterfaceHelper
{
    public static class ChunkFinder
    {
        public static ChunkComponent[] GetAllChunkComponents()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            NativeArray<Entity> chunks = entityManager.CreateEntityQuery(typeof(ChunkComponent)).ToEntityArray(Allocator.Temp);

            List<ChunkComponent> chunkPositions = new();

            foreach (Entity entity in chunks)
                chunkPositions.Add(entityManager.GetComponentData<ChunkComponent>(entity));

            return chunkPositions.ToArray();
        }
    }
}