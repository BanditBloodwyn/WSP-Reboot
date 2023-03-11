using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.Generation.GenerationComponents.Terrain.Math;
using Assets._Project._Scripts.World.Generation.Settings;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.GenerationComponents.Terrain
{
    public class TerrainBuilderComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;
        [SerializeReference] private NoiseParameters _noiseParameters;

        public bool Enabled => _enabled;

        public void Apply(Chunk chunk)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

            foreach (Entity tileEntity in chunk.Tiles)
            {
                EmptyTileAspect tileAspect = entityManager.GetAspect<EmptyTileAspect>(tileEntity);
                
                float height = CalculateHeight(tileAspect.Position.x, tileAspect.Position.z);
              
                entityManager.SetComponentData(
                    tileAspect.Entity,
                    new LocalTransform { Position = new float3(tileAspect.Position.x, height, tileAspect.Position.z) });
            }
        }

        private float CalculateHeight(float x, float y)
        {
            PerlinNoiseEvaluator evaluator = new PerlinNoiseEvaluator();
            float firstLayerValue = 0;
            float elevation = 0;
            float3 point = new float3(x, 0, y);

            if (_noiseParameters.NoiseFilters.Length > 0)
            {
                firstLayerValue = _noiseParameters.NoiseFilters[0].Evaluate(point, evaluator);
                elevation = firstLayerValue;
            }

            if (_noiseParameters.NoiseFilters.Length == 1)
                return elevation;

            for (int i = 1; i < _noiseParameters.NoiseFilters.Length; i++)
            {
                float mask = firstLayerValue;
                elevation += _noiseParameters.NoiseFilters[i].Evaluate(point, evaluator) * mask;
            }

            return elevation;
        }

    }
}