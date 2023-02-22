using Assets._Project._Scripts.World.ECS.Components;
using Assets._Project._Scripts.World.Generation.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.World.Generation
{
    public static class TileBuilder
    {
        public static Entity Build(EntityManager entityManager, float x, float y, long chunkID, WorldCreationParameters parameters)
        {
            Entity tile = entityManager.CreateEntity();

            float height = CalculateHeight(x, y, parameters);

            entityManager.AddComponent<LocalTransform>(tile);
            entityManager.SetComponentData(tile, new LocalTransform {Position = new float3(x, height, y) });

            entityManager.AddComponent<ChunkAssignmentComponent>(tile);
            entityManager.SetComponentData(tile, new ChunkAssignmentComponent {ChunkID = chunkID});
            return tile;
        }

        private static float CalculateHeight(float x, float y, WorldCreationParameters parameters)
        {
            PerlinNoiseEvaluator evaluator = new PerlinNoiseEvaluator();
            float firstLayerValue = 0;
            float elevation = 0;
            float3 point = new float3(x, 0, y);

            if (parameters.NoiseFilters.Length > 0)
            {
                firstLayerValue = parameters.NoiseFilters[0].Evaluate(point, evaluator);
                elevation = firstLayerValue;
            }

            if (parameters.NoiseFilters.Length == 1)
                return elevation;

            for (int i = 1; i < parameters.NoiseFilters.Length; i++)
            {
                float mask = firstLayerValue;
                elevation += parameters.NoiseFilters[i].Evaluate(point, evaluator) * mask;
            }

            return elevation;

        }
    }
}