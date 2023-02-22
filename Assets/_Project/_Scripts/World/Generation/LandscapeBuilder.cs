using System.Collections.Generic;
using Assets._Project._Scripts.World.Components;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public static class LandscapeBuilder
    {
        public static void Build(WorldCreationParameters parameters, Transform chunkParent, Material tileMaterial)
        {
            List<ChunkComponent> chunkComponents = new List<ChunkComponent>();

            for (int x = 0; x < parameters.WorldSize; x++)
            {
                for (int y = 0; y < parameters.WorldSize; y++)
                {
                    ChunkComponent chunk = ChunkBuilder.Build(x, y, parameters, tileMaterial);
                    chunk.transform.parent = chunkParent;
                    chunkComponents.Add(chunk);
                }
            }

            foreach (ChunkComponent chunkComponent in chunkComponents)
            {
                chunkComponent.Tiles = ChunkBuilder.BuildTiles(chunkComponent, parameters);
            }

            foreach (ChunkComponent chunkComponent in chunkComponents)
            {
                MeshFilter meshFilter = chunkComponent.gameObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = ChunkBuilder.CreateChunkMesh(chunkComponent);

                MeshRenderer renderer = chunkComponent.gameObject.AddComponent<MeshRenderer>();
                renderer.sharedMaterial = tileMaterial;

                MeshCollider collider = chunkComponent.gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = meshFilter.sharedMesh;
            }
        }
    }
}