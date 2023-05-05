using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using System.Linq;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.ChunkObject
{
    public static class ChunkObjectGenerator
    {
        public static void Generate(Chunk chunk, WorldCreationParameters settings)
        {
            GameObject chunkObject = new($"Chunk{chunk.ID}");
            chunkObject.transform.position = new Vector3(chunk.Coordinates.x * chunk.Size, 0, chunk.Coordinates.y * chunk.Size);
            chunkObject.transform.parent = settings.ChunkParent != null
                ? settings.ChunkParent
                : GameObject.Find("#Chunks").transform;

            MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = chunk.Mesh;

            MeshRenderer renderer = chunkObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = settings.DefaultWorldMaterial;

            MeshCollider collider = chunkObject.AddComponent<MeshCollider>();
            collider.sharedMesh = meshFilter.sharedMesh;

            ChunkComponent chunkComponent = chunkObject.AddComponent<ChunkComponent>();
            chunkComponent.Tiles = chunk.Tiles;
            chunkComponent.ID = chunk.ID;

            AdjustMaterial(renderer.sharedMaterial, settings.VegetationZoneSettings);
        }

        private static void AdjustMaterial(Material material, VegetationZoneSettings settings)
        {
            if(settings.VegetationZones == null || settings.VegetationZones.Length == 0)
                return;

            material.SetFloat("_WaterHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Water).MaximumHeight);
            material.SetFloat("_KollineHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Kolline).MaximumHeight);
            material.SetFloat("_MontaneHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Montane).MaximumHeight);
            material.SetFloat("_SubalpineHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Subalpine).MaximumHeight);
            material.SetFloat("_Alpine_TreesHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Alpine_Trees).MaximumHeight);
            material.SetFloat("_Alpine_BushesHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Alpine_Bushes).MaximumHeight);
            material.SetFloat("_SubnivaleHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Subnivale).MaximumHeight);
            material.SetFloat("_NivaleHeight", settings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Nivale).MaximumHeight);
        }
    }
}