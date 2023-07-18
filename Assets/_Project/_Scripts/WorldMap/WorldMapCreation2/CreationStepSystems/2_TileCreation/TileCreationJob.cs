﻿using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Burst;
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
            for (int x = 0; x < chunk.TileAmountPerAxis; x++)
            {
                for (int y = 0; y < chunk.TileAmountPerAxis; y++)
                {
                    CreateTileEntity(chunkIndex,
                        x + chunk.TileAmountPerAxis * chunk.Coordinates.x - chunk.TileAmountPerAxis / 2,
                        y + chunk.TileAmountPerAxis * chunk.Coordinates.y - chunk.TileAmountPerAxis / 2,
                        chunk.Coordinates.y * chunk.TileAmountPerAxis + chunk.Coordinates.x);
                }
            }
        }

        [BurstCompile]
        private void CreateTileEntity(int chunkIndex, int xPos, int yPos, int chunkID)
        {
            Entity tileEntity = EntityCommandBuffer.CreateEntity(chunkIndex);

            EntityCommandBuffer.AddComponent<LocalTransform>(chunkIndex, tileEntity);
            EntityCommandBuffer.SetComponent(chunkIndex, tileEntity, new LocalTransform { Position = new float3(xPos, 0, yPos) });

            EntityCommandBuffer.AddComponent<ChunkAssignmentComponentData>(chunkIndex, tileEntity);
            EntityCommandBuffer.SetComponent(chunkIndex, tileEntity, new ChunkAssignmentComponentData { ChunkID = chunkID });
        }
    }
}