using System.Collections.Generic;
using Assets._Project._Scripts.World.Components;
using Assets._Project._Scripts.World.Generation.Helper;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public static class LandscapeBuilder
    {
        public static void Build(WorldCreationParameters parameters, Transform chunkParent, Material tileMaterial)
        {
            Chunk[] chunks = CreateChunkData(parameters, tileMaterial);
            Chunk[] filledChunks = FillChunksWithTiles(parameters, chunks, out float maximumHeight, out float minimumHeight);
            MaterialAdjuster.AdjustMaterialSettings(parameters, tileMaterial, maximumHeight, minimumHeight);
            CreateChunkGameObjects(chunkParent, tileMaterial, filledChunks);
        }

        private static Chunk[] CreateChunkData(
            WorldCreationParameters parameters, 
            Material tileMaterial)
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

            return chunks.ToArray();
        }

        private static Chunk[] FillChunksWithTiles(
            WorldCreationParameters parameters, 
            Chunk[] chunks, 
            out float maximumHeight,
            out float minimumHeight)
        {
            List<Chunk> filledChunks = new();
            maximumHeight = float.NegativeInfinity;
            minimumHeight = float.PositiveInfinity;

            foreach (Chunk chunk in chunks)
            {
                Chunk currentChunk = chunk;

                currentChunk.Tiles =
                    ChunkBuilder.BuildTiles(currentChunk, parameters, out float minHeight, out float maxHeight);
                currentChunk.Mesh = ChunkBuilder.CreateChunkMesh(currentChunk);

                if (maxHeight > maximumHeight)
                    maximumHeight = maxHeight;
                if (minHeight < minimumHeight)
                    minimumHeight = minHeight;

                filledChunks.Add(currentChunk);
            }

            return filledChunks.ToArray();
        }

        private static void CreateChunkGameObjects(Transform chunkParent, Material tileMaterial, Chunk[] filledChunks)
        {
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