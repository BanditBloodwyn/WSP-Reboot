using Assets._Project._Scripts.World.Components;
using Assets._Project._Scripts.World.Data.Enums;
using Assets._Project._Scripts.World.Generation.Helper;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public static class LandscapeBuilder
    {
        public static void Build(WorldCreationParameters parameters, Transform chunkParent, Material tileMaterial)
        {
            Chunk[] chunks = CreateChunkData(parameters);
            Chunk[] filledChunks = FillChunksWithTiles(parameters, chunks, out float maximumHeight);

            Dictionary<VegetationZones, float> vegetationZoneHeights = VegetationZoneInterpolator.GetVegetationZoneHeights(parameters, maximumHeight);
            FillTileData(filledChunks, vegetationZoneHeights);
            AdjustMaterialSettings(tileMaterial, vegetationZoneHeights);

            CreateChunkGameObjects(chunkParent, tileMaterial, filledChunks);
        }

        private static Chunk[] CreateChunkData(WorldCreationParameters parameters)
        {
            List<Chunk> chunks = new List<Chunk>();

            for (int x = 0; x < parameters.WorldSize; x++)
            {
                for (int y = 0; y < parameters.WorldSize; y++)
                {
                    Chunk chunk = ChunkBuilder.Build(x, y, parameters);
                    chunks.Add(chunk);
                }
            }

            return chunks.ToArray();
        }

        private static Chunk[] FillChunksWithTiles(WorldCreationParameters parameters, Chunk[] chunks, out float maximumHeight)
        {
            List<Chunk> filledChunks = new();
            maximumHeight = float.NegativeInfinity;

            foreach (Chunk chunk in chunks)
            {
                Chunk currentChunk = chunk;

                currentChunk.Tiles = ChunkBuilder.BuildTiles(currentChunk, parameters, out float maxHeight);
                currentChunk.Mesh = ChunkBuilder.CreateChunkMesh(currentChunk);

                if (maxHeight > maximumHeight)
                    maximumHeight = maxHeight;

                filledChunks.Add(currentChunk);
            }

            return filledChunks.ToArray();
        }

        private static void FillTileData(Chunk[] chunks, Dictionary<VegetationZones, float> vegetationZoneHeights)
        {
            foreach (Chunk chunk in chunks)
                ChunkBuilder.FillTileData(chunk, vegetationZoneHeights);
        }

        private static void AdjustMaterialSettings(Material tileMaterial, IReadOnlyDictionary<VegetationZones, float> values)
        {
            tileMaterial.SetFloat("_WaterHeight", values[VegetationZones.Water]);
            tileMaterial.SetFloat("_KollineHeight", values[VegetationZones.Kolline]);
            tileMaterial.SetFloat("_MontaneHeight", values[VegetationZones.Montane]);
            tileMaterial.SetFloat("_SubalpineHeight", values[VegetationZones.Subalpine]);
            tileMaterial.SetFloat("_Alpine_TreesHeight", values[VegetationZones.Alpine_Trees]);
            tileMaterial.SetFloat("_Alpine_BushesHeight", values[VegetationZones.Alpine_Bushes]);
            tileMaterial.SetFloat("_SubnivaleHeight", values[VegetationZones.Subnivale]);
            tileMaterial.SetFloat("_NivaleHeight", values[VegetationZones.Nivale]);
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