using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.WorldMap
{
    public static class TileMethods
    {
        public static EmptyTileAspect GetEmptyTileAspect(this Entity entity)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
            return entityManager.GetAspect<EmptyTileAspect>(entity);
        }

        public static void SetTilePosition(this Entity entity, float3 position)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.SetComponentData(entity, new LocalTransform { Position = position });
        }

        public static void AddTileProperties(this Entity entity, TilePropertiesComponentData data) 
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.AddComponent<TilePropertiesComponentData>(entity);
            entityManager.SetComponentData(entity, data);
        }
    }
}