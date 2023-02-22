using System.Collections.Generic;
using Assets._Project._Scripts.World.Components;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public static class LandscapeBuilder
    {
        public static void Build(WorldCreationParameters parameters, Transform chunkParent, Material tileMaterial)
        {
            List<Chunk> chunks = new List<Chunk>();

            for (int x = 0; x < parameters.WorldSize; x++)
            {
                for (int y = 0; y < parameters.WorldSize; y++)
                {
                    Chunk chunk = ChunkBuilder.Build(x, y, parameters, tileMaterial);
                    chunks.Add(chunk);
                }
            }

            List<Chunk> filledChunks = new List<Chunk>();

            foreach (Chunk chunk in chunks)
            {
                Chunk currentChunk = chunk;

                currentChunk.Tiles = ChunkBuilder.BuildTiles(currentChunk, parameters);
                currentChunk.Mesh = ChunkBuilder.CreateChunkMesh(currentChunk);

                filledChunks.Add(currentChunk);
            }

            foreach (Chunk chunk in filledChunks)
            {
                GameObject chunkObject = new GameObject($"Chunk{chunk.ID}");
                chunkObject.transform.position = new(chunk.Coordinates.x * chunk.Size, 0, chunk.Coordinates.y * chunk.Size);
                chunkObject.transform.parent = chunkParent;

                MeshFilter meshFilter = chunkObject.gameObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = chunk.Mesh;

                MeshRenderer renderer = chunkObject.gameObject.AddComponent<MeshRenderer>();
                renderer.sharedMaterial = tileMaterial;

                MeshCollider collider = chunkObject.gameObject.AddComponent<MeshCollider>();
                collider.sharedMesh = meshFilter.sharedMesh;
            }
        }
    }
}