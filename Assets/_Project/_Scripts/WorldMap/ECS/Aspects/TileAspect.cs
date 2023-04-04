using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.ECS.Aspects
{
    public readonly partial struct TileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly TransformAspect _transformAspect;

        private readonly RefRO<ChunkAssignmentComponentData> _chunkAssignment;
        private readonly RefRW<TilePropertiesComponentData> _tileProperties;

        public float3 Position => _transformAspect.LocalPosition;

        public TilePropertiesComponentData GetData()
        {
            return _tileProperties.ValueRO;
        }

        public string GetVegetationZone()
        {
            return _tileProperties.ValueRO.VegetationZone.ToString();
        }

        public float GetDeciduousTrees()
        {
            return _tileProperties.ValueRO.FloraValues.DeciduousTrees;
        }

        public float GetEvergreenTrees()
        {
            return _tileProperties.ValueRO.FloraValues.EvergreenTrees;
        }
      
        public float GetVegetables()
        {
            return _tileProperties.ValueRO.FloraValues.Vegetables;
        }
       
        public float GetFruits()
        {
            return _tileProperties.ValueRO.FloraValues.Fruits;
        }
    }
}