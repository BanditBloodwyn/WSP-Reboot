using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Settings.Scriptables;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Helper;
using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2
{
    public class WorldCreator : MonoBehaviour
    {
        [SerializeField] private WorldCreationParameters _worldCreationParameters;

        private void OnEnable()
        {
            Events.OnStartWorldCreation.AddListener(StartWorldGeneration);
        }

        private void OnDisable()
        {
            Events.OnStartWorldCreation.RemoveListener(StartWorldGeneration);
        }

        [Button]
        private object StartWorldGeneration(Component sender, object data)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            Entity entity = entityManager.CreateEntity();
            entityManager.SetName(entity, "WorldCreationControlEntity");
            entityManager.AddComponent<WorldCreationControlComponent>(entity);

            CreateWorldCreationParametersComponent(ref entityManager, ref entity);

            return null;
        }

        private void CreateWorldCreationParametersComponent(ref EntityManager entityManager, ref Entity entity)
        {
            entityManager.AddComponent<WorldCreationParametersComponent>(entity);
            entityManager.SetComponentData(entity, WorldCreationParametersCreator.Create(_worldCreationParameters));
        }
    }
}
