using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.World.Components;
using Assets._Project._Scripts.World.Data.Enums;
using Assets._Project._Scripts.World.Data.Structs;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.old
{
    public class ChunkBuilder
    {
        public static void Build(int xCoord, int yCoord, WorldCreationParameters worldCreationParameters, Transform parent, Material material)
        {
            Chunk chunk = new Chunk();
            BuildChunkData(ref chunk, xCoord, yCoord, worldCreationParameters);
            FillChunksWithTiles(ref chunk, worldCreationParameters);
            FillTileData(ref chunk, worldCreationParameters);
            ChunkMeshBuilder.Build(ref chunk);
            CreateChunkGameObject(ref chunk, parent, material);
        }

        private static void BuildChunkData(ref Chunk chunk, int xCoord, int yCoord, WorldCreationParameters worldCreationParameters)
        {
            chunk.ID = yCoord * worldCreationParameters.ChunkSize + xCoord;
            chunk.Coordinates = new Vector2Int(xCoord, yCoord);
            chunk.Size = worldCreationParameters.ChunkSize;

            World.Instance.Chunks.Add(chunk);
        }

        private static void FillChunksWithTiles(ref Chunk chunk, WorldCreationParameters worldCreationParameters)
        {
            List<Entity> entities = new();

            for (int x = 0; x < chunk.Size; x++)
            {
                for (int y = 0; y < chunk.Size; y++)
                {
                    Entity tile = TileBuilder.Build(
                        x + chunk.Size * chunk.Coordinates.x - chunk.Size / 2,
                        y + chunk.Size * chunk.Coordinates.y - chunk.Size / 2,
                        chunk.Coordinates.y * chunk.Size + chunk.Coordinates.x,
                        worldCreationParameters);

                    entities.Add(tile);
                }
            }

            chunk.Tiles = entities.ToArray();
        }

        private static void FillTileData(ref Chunk chunk, WorldCreationParameters worldCreationParameters)
        {
            foreach (Entity tile in chunk.Tiles)
                TileBuilder.FillTileData(tile, worldCreationParameters);
        }

        public static void AdjustMaterialSettings(Material material, VegetationZoneHeight[] vegetationZoneHeights)
        {
            material.SetFloat("_WaterHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Water).MaximumHeight);
            material.SetFloat("_KollineHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Kolline).MaximumHeight);
            material.SetFloat("_MontaneHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Montane).MaximumHeight);
            material.SetFloat("_SubalpineHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Subalpine).MaximumHeight);
            material.SetFloat("_Alpine_TreesHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Alpine_Trees).MaximumHeight);
            material.SetFloat("_Alpine_BushesHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Alpine_Bushes).MaximumHeight);
            material.SetFloat("_SubnivaleHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Subnivale).MaximumHeight);
            material.SetFloat("_NivaleHeight", vegetationZoneHeights.First(static zone => zone.VegetationZone == VegetationZones.Nivale).MaximumHeight);
        }

        private static void CreateChunkGameObject(ref Chunk chunk, Transform parent, Material material)
        {
            GameObject chunkObject = new($"Chunk{chunk.ID}");
            chunkObject.transform.position = new Vector3(chunk.Coordinates.x * chunk.Size, 0, chunk.Coordinates.y * chunk.Size);
            chunkObject.transform.parent = parent;

            MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = chunk.Mesh;

            MeshRenderer renderer = chunkObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = material;

            MeshCollider collider = chunkObject.AddComponent<MeshCollider>();
            collider.sharedMesh = meshFilter.sharedMesh;

            ChunkComponent chunkComponent = chunkObject.AddComponent<ChunkComponent>();
            chunkComponent.Tiles = chunk.Tiles;
            chunkComponent.ID = chunk.ID;
        }
    }
}