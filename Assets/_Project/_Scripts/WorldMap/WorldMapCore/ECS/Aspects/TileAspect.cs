﻿using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects
{
    public readonly partial struct TileAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;

        private readonly RefRO<ChunkAssignmentComponentData> _chunkAssignment;
        private readonly RefRW<TilePropertiesComponentData> _tileProperties;

        public float3 Position => _localTransform.ValueRO.Position;

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

        public float GetCoal()
        {
            return _tileProperties.ValueRO.ResourceValues.Coal;
        }

        public float GetIron()
        {
            return _tileProperties.ValueRO.ResourceValues.IronOre;
        }

        public float GetGold()
        {
            return _tileProperties.ValueRO.ResourceValues.GoldOre;
        }

        public float GetHerbivores()
        {
            return _tileProperties.ValueRO.FaunaValues.Herbivores;
        }

        public float GetCarnivores()
        {
            return _tileProperties.ValueRO.FaunaValues.Carnivores;
        }

        public float GetUrbanization()
        {
            return _tileProperties.ValueRO.PopulationValues.Urbanization;
        }

        public float GetLifeStandard()
        {
            return _tileProperties.ValueRO.PopulationValues.LifeStandard;
        }
    }
}