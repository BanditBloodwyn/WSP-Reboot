using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using EmptyTileAspect = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects.EmptyTileAspect;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation
{
    public static class TileMethods
    {
        public static EmptyTileAspect GetEmptyTileAspect(this Entity entity)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            return entityManager.GetAspect<EmptyTileAspect>(entity);
        }

        public static void SetTilePosition(this Entity entity, float3 position)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.SetComponentData(entity, new LocalTransform { Position = position });
        }

        public static void AddTileProperties(this Entity entity, TilePropertiesComponentData data)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.AddComponent<TilePropertiesComponentData>(entity);
            entityManager.SetComponentData(entity, data);
        }
    }
}