using System;
using System.Collections.Generic;
using Unity.Entities;

namespace Assets._Project._Scripts.Core.Data.ECS
{
    public static class AspectAccessor
    {
        public static T GetAspectOfEntity<T>(Entity entity)
            where T : struct, IAspect, IAspectCreate<T>
        {
            return World.DefaultGameObjectInjectionWorld.EntityManager.GetAspect<T>(entity);
        }

        public static T[] GetAspectsOfEntitiesWithComponents<T>(params ComponentType[] components)
            where T : struct, IAspect, IAspectCreate<T>
        {
            Entity[] entities = EntityAccessor.GetAllEntitiesWithComponents(components);

            if (entities.Length == 0)
                return Array.Empty<T>();

            List<T> aspects = new();

            foreach (Entity entity in entities)
                aspects.Add(GetAspectOfEntity<T>(entity));

            return aspects.ToArray();
        }
    }
}