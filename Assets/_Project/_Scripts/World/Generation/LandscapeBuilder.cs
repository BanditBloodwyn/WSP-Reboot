using System.Collections.Generic;
using Assets._Project._Scripts.World.Components;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public static class LandscapeBuilder
    {
        public static void Build(WorldCreationParameters parameters, Transform chunkParent, Material tileMaterial)
        {
            List<Chunk> chunks = new();

            for (int x = 0; x < parameters.WorldSize; x++)
            {
                for (int y = 0; y < parameters.WorldSize; y++)
                {
                    Chunk chunk = ChunkBuilder.Build(x, y, parameters, tileMaterial);
                    chunks.Add(chunk);
                }
            }

            List<Chunk> filledChunks = new();

            foreach (Chunk chunk in chunks)
            {
                Chunk currentChunk = chunk;

                currentChunk.Tiles = ChunkBuilder.BuildTiles(currentChunk, parameters);
                currentChunk.Mesh = ChunkBuilder.CreateChunkMesh(currentChunk);

                filledChunks.Add(currentChunk);
            }

            foreach (Chunk chunk in filledChunks)
            {
                GameObject chunkObject = new($"Chunk{chunk.ID}");
                chunkObject.transform.position = new Vector3(chunk.Coordinates.x * chunk.Size, 0, chunk.Coordinates.y * chunk.Size);
                chunkObject.transform.parent = chunkParent;

                MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
                meshFilter.sharedMesh = chunk.Mesh;

                MeshRenderer renderer = chunkObject.AddComponent<MeshRenderer>();
                renderer.sharedMaterial = tileMaterial;

                MeshCollider collider = chunkObject.AddComponent<MeshCollider>();
                collider.sharedMesh = meshFilter.sharedMesh;

                ChunkComponent chunkComponent = chunkObject.AddComponent<ChunkComponent>();
                chunkComponent.Tiles = chunk.Tiles;
                chunkComponent.ID = chunk.ID;
            }
        }
    }
}