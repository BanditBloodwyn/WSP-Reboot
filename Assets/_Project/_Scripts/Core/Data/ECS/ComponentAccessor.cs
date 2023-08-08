using Unity.Collections;
using Unity.Entities;

namespace Assets._Project._Scripts.Core.Data.ECS
{
    public static class ComponentAccessor
    {
        public static T[] GetAllComponents<T>()
            where T : unmanaged, IComponentData
        {
            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(T));

            return entityQuery
                .ToComponentDataArray<T>(Allocator.Temp)
                .ToArray();
        }

        public static T GetComponentOfEntity<T>(Entity entity)
            where T : unmanaged, IComponentData
        {
            return World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<T>(entity);
        }
    }
}