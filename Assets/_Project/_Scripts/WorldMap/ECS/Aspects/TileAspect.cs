using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.ECS.Aspects
{
    public readonly partial struct TileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;

        private readonly RefRO<ChunkAssignmentComponentData> _chunkAssignment;
        private readonly RefRW<TilePropertiesComponentData> _tileProperties;

        public float3 Position => _localTransform.ValueRO.Position;

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
      
        public float GetHerbs()
        {
            return _tileProperties.ValueRO.FloraValues.Herbs;
        }
       
        public float GetFruits()
        {
            return _tileProperties.ValueRO.FloraValues.Fruits;
        }

        public float GetOil()
        {
            return _tileProperties.ValueRO.ResourceValues.Oil;
        }

        public float GetGas()
        {
            return _tileProperties.ValueRO.ResourceValues.Gas;
        }
    }
}