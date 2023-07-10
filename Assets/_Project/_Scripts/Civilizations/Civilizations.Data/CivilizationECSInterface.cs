using Assets._Project._Scripts.Civilizations.Civilizations.Data.Entities;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldMapManagement;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.Civilizations.Civilizations.Data
{
    public static class CivilizationECSInterface
    {
        public static bool CreateCivilization(string name, Color color, Vector3 center)
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity civilization = entityManager.CreateEntity();

            entityManager.SetName(civilization, new FixedString64Bytes($"civilization_{name}"));

            entityManager.AddComponent<CivilizationComponent>(civilization);

            CivilizationComponent civilizationComponent = new();
            civilizationComponent.Name = new FixedString64Bytes(name);
            civilizationComponent.Color = color;

            civilizationComponent.OwnedTiles = new NativeList<TileAspect>(Allocator.Persistent);
            if (TryGetTile(center, out TileAspect tile))
                civilizationComponent.OwnedTiles.Add(tile);

            entityManager.SetComponentData(civilization, civilizationComponent);

            return true;
        }

        private static bool TryGetTile(float3 centerOfArea, out TileAspect tile)
        {
            return WorldInterface.Instance.TryGetTileFromPosition(centerOfArea, out tile);
        }
    }
}