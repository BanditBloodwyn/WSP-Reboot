using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using ChunkComponent = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components.ChunkComponent;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.CreationStepSystems._2_TileCreation
{
    [BurstCompile]
    public partial struct TileCreationJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter EntityCommandBuffer;

        [BurstCompile]
        private void Execute([ChunkIndexInQuery] int chunkIndex, ref ChunkComponent chunk)
        {
            NativeArray<Entity> entities = new NativeArray<Entity>(chunk.TileAmountPerAxis * chunk.TileAmountPerAxis, Allocator.Temp);
            Entity sourceTile = CreateSourceTileEntity(chunkIndex);
            EntityCommandBuffer.Instantiate(chunkIndex, sourceTile, entities);

            for (int x = 0; x < chunk.TileAmountPerAxis; x++)
            {
                for (int y = 0; y < chunk.TileAmountPerAxis; y++)
                {
                    SetTileData(
                        chunkIndex,
                        entities[x * chunk.TileAmountPerAxis + y],
                        x + chunk.TileAmountPerAxis * chunk.Coordinates.x - chunk.TileAmountPerAxis / 2,
                        y + chunk.TileAmountPerAxis * chunk.Coordinates.y - chunk.TileAmountPerAxis / 2,
                        chunk.Coordinates.y * chunk.TileAmountPerAxis + chunk.Coordinates.x);
                }
            }

            EntityCommandBuffer.DestroyEntity(chunkIndex, sourceTile);
            entities.Dispose();
        }

        [BurstCompile]
        private Entity CreateSourceTileEntity(int chunkIndex)
        {
            Entity tileEntity = EntityCommandBuffer.CreateEntity(chunkIndex);
            EntityCommandBuffer.SetName(chunkIndex, tileEntity, "tileSource");
            EntityCommandBuffer.AddComponent<LocalTransform>(chunkIndex, tileEntity);
            EntityCommandBuffer.AddComponent<ChunkAssignmentComponentData>(chunkIndex, tileEntity);

            return tileEntity;
        }

        [BurstCompile]
        private void SetTileData(int chunkIndex, Entity tile, int xPos, int yPos, int chunkID)
        {
            EntityCommandBuffer.SetComponent(chunkIndex, tile, new LocalTransform { Position = new float3(xPos, 0, yPos) });
            EntityCommandBuffer.SetComponent(chunkIndex, tile, new ChunkAssignmentComponentData { ChunkID = chunkID });
        }
    }
}