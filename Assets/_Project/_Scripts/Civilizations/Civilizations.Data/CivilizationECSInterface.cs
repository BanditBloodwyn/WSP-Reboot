using Assets._Project._Scripts.Civilizations.Civilizations.Data.Entities;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.Civilizations.Civilizations.Data
{
    public static class CivilizationECSInterface
    {
        public static bool CreateCivilization(string name, Color color)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity civilization = entityManager.CreateEntity();

            entityManager.SetName(civilization, new FixedString64Bytes($"civilization_{name}"));

            entityManager.AddComponent<CivilizationComponent>(civilization);
            entityManager.SetComponentData(civilization, new CivilizationComponent
            {
                Name = new FixedString64Bytes(name),
                Color = color
            });

            return true;
        }
    }
}