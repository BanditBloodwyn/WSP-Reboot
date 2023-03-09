using Assets._Project._Scripts.World.Data.Enums;
using Assets._Project._Scripts.World.Generation.Settings;
using System.Linq;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.GenerationComponents
{
    public class GameObjectBuilderComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private Transform _chunkParent;
        [SerializeField] private Material _tileMaterial;
        [SerializeField] private VegetationZoneSettings _vegetationZoneSettings;

        public bool Enabled => _enabled;

        public void Apply(Chunk chunk)
        {
            GameObject chunkObject = new($"Chunk{chunk.ID}");
            chunkObject.transform.position = new Vector3(chunk.Coordinates.x * chunk.Size, 0, chunk.Coordinates.y * chunk.Size);
            chunkObject.transform.parent = _chunkParent;

            MeshFilter meshFilter = chunkObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = chunk.Mesh;

            MeshRenderer renderer = chunkObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = _tileMaterial;

            MeshCollider collider = chunkObject.AddComponent<MeshCollider>();
            collider.sharedMesh = meshFilter.sharedMesh;

            ChunkComponent chunkComponent = chunkObject.AddComponent<ChunkComponent>();
            chunkComponent.Tiles = chunk.Tiles;
            chunkComponent.ID = chunk.ID;

            AdjustMaterial();
        }

        private void AdjustMaterial()
        {
            _tileMaterial.SetFloat("_WaterHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Water).MaximumHeight);
            _tileMaterial.SetFloat("_KollineHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Kolline).MaximumHeight);
            _tileMaterial.SetFloat("_MontaneHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Montane).MaximumHeight);
            _tileMaterial.SetFloat("_SubalpineHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Subalpine).MaximumHeight);
            _tileMaterial.SetFloat("_Alpine_TreesHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Alpine_Trees).MaximumHeight);
            _tileMaterial.SetFloat("_Alpine_BushesHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Alpine_Bushes).MaximumHeight);
            _tileMaterial.SetFloat("_SubnivaleHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Subnivale).MaximumHeight);
            _tileMaterial.SetFloat("_NivaleHeight", _vegetationZoneSettings.VegetationZones.First(static zone => zone.VegetationZone == VegetationZones.Nivale).MaximumHeight);
        }
    }
}