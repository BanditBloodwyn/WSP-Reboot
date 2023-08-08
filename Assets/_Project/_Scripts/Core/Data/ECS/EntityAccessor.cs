using Unity.Collections;
using Unity.Entities;

namespace Assets._Project._Scripts.Core.Data.ECS
{
    public static class EntityAccessor
    {
        public static Entity[] GetAllEntitiesWithComponents(params ComponentType[] components)
        {
            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(components);

            return entityQuery
                .ToEntityArray(Allocator.Temp)
                .ToArray();
        }

    }
}