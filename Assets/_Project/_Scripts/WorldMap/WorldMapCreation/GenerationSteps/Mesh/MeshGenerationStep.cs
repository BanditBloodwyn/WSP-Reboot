﻿using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Height;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;
using Unity.Entities;
using Unity.Mathematics;
using EmptyTileAspect = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects.EmptyTileAspect;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Mesh
{
    public class MeshGenerationStep : IWorldGenerationStep
    {
        public List<Type> RequiredDependencies => new() { typeof(HeightGenerationStep) };

        public IEnumerator Process(WorldContext context, WorldCreationParameters settings)
        {
            foreach (Chunk chunk in context.Chunks)
            {
                List<float3> tilePositions = new();

                foreach (Entity tile in chunk.Tiles)
                {
                    EmptyTileAspect tileAspect = tile.GetEmptyTileAspect();
                    tilePositions.Add(tileAspect.Position);
                }

                chunk.Mesh = MeshGenerator.GenerateMeshFromVoxelPositions(
                    chunk,
                    tilePositions.ToArray());

                yield return null;
            }
        }
    }
}