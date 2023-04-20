using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.TileData
{
    public class TileDataBuilderComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;
        [SerializeField] private VegetationZoneSettings _vegetationZoneSettings;
       
        [SerializeField] private FloraGenerator _floraGenerator;
        [SerializeField] private FaunaGenerator _faunaGenerator;
        [SerializeField] private ResourceGenerator _resourceGenerator;

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
            
            data.TerrainValues.Height = height;
            data.X = (int)tileAspect.Position.x;
            data.Z = (int)tileAspect.Position.z;

            data.VegetationZone = GetVegetationZoneByHeight(height);

            data.FloraValues = _floraGenerator.Generate(data, tileAspect.Position);
            data.FaunaValues = _faunaGenerator.Generate(data);
            data.ResourceValues = _resourceGenerator.Generate(data, tileAspect.Position);
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