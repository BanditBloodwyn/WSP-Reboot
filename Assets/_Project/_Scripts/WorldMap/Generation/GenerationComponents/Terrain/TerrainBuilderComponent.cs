using Assets._Project._Scripts.Core.Math.Noise;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.GenerationPipeline;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.Terrain
{
    public class TerrainBuilderComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;
        [SerializeReference] private NoiseParameters _noiseParameters;

        public bool Enabled => _enabled;

        public void Apply(Chunk chunk)
        {
            foreach (Entity tileEntity in chunk.Tiles)
            {
                EmptyTileAspect tileAspect = tileEntity.GetEmptyTileAspect();
                
                float height = CalculateHeight(tileAspect.Position.x, tileAspect.Position.z);
                tileAspect.Entity.SetTilePosition(new float3(tileAspect.Position.x, height, tileAspect.Position.z));
            }
        }

        private float CalculateHeight(float x, float y)
        {
            PerlinNoiseEvaluator evaluator = new();
            float firstLayerValue = 0;
            float elevation = 0;
            float3 point = new(x, 0, y);

            if (_noiseParameters.NoiseFilters.Length > 0)
            {
                firstLayerValue = _noiseParameters.NoiseFilters[0].Evaluate(point, evaluator);
                elevation = firstLayerValue;
            }

            if (_noiseParameters.NoiseFilters.Length == 1)
                return elevation;

            for (int i = 1; i < _noiseParameters.NoiseFilters.Length; i++)
                elevation += _noiseParameters.NoiseFilters[i].Evaluate(point, evaluator) * firstLayerValue;

            return elevation;
        }

    }
}