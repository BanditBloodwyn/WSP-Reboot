using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets._Project._Scripts.World
{
    public static class TileMethods
    {
        private static EntityManager _entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

        public static EmptyTileAspect GetEmptyTileAspect(this Entity entity)
        {
            return _entityManager.GetAspect<EmptyTileAspect>(entity);
        }

        public static void SetTilePosition(this Entity entity, float3 position)
        {
            _entityManager.SetComponentData(entity, new LocalTransform { Position = position });
        }

        public static void AddTileProperties(this Entity entity, TilePropertiesComponentData data) 
        {
            _entityManager.AddComponent<TilePropertiesComponentData>(entity);
            _entityManager.SetComponentData(entity, data);
        }
    }
}