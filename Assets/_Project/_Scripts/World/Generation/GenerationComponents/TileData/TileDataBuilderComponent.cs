using Assets._Project._Scripts.World.Data.Enums;
using Assets._Project._Scripts.World.Data.Structs;
using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.ECS.Components;
using Assets._Project._Scripts.World.Generation.Settings;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.GenerationComponents.TileData
{
    public class TileDataBuilderComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private VegetationZoneSettings _vegetationZoneSettings;

        public bool Enabled => _enabled;

        public void Apply(Chunk chunk)
        {
            foreach (Entity tile in chunk.Tiles)
                tile.AddTileProperties(BuildTileProperties(tile));
        }

        private TilePropertiesComponentData BuildTileProperties(Entity tile)
        {
            EmptyTileAspect tileAspect = tile.GetEmptyTileAspect();
            float height = tileAspect.Position.y;

            TilePropertiesComponentData data = new TilePropertiesComponentData();

            data.VegetationZone = GetVegetationZoneByHeight(height);

            data.TerrainValues = new TerrainValues();
            data.FloraValues = new FloraValues();
            data.FaunaValues = new FaunaValues();
            data.PopulationValues = new PopulationValues();

            return data;
        }

        private VegetationZones GetVegetationZoneByHeight(float height)
        {
            foreach (VegetationZoneHeight zone in _vegetationZoneSettings.VegetationZones)
            {
                if (height > zone.MaximumHeight)
                    continue;
                return zone.VegetationZone;
            }

            return VegetationZones.Water;
        }
    }
}