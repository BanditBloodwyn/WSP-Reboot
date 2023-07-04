using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldPrefabs;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets._Project._Scripts.CoreFeatures.ObjectPlacementSystem
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField] private Transform _placablesParent;


        #region Unity

        private void OnEnable()
        {
            Events.OnRequestBuildTemplateBuilding.AddListener(PlaceCity);
        }

        private void OnDisable()
        {
            Events.OnRequestBuildTemplateBuilding.RemoveListener(PlaceCity);
        }

        #endregion


        private object PlaceCity(Component component, object o)
        {
            return Place(component, o, WorldPrefabNames.City);
        }

        private object Place(Component sender, object data, WorldPrefabNames prefabName)
        {
            if (data is not TileAspect tile)
                return null;

            if (!WorldPrefabs.Instance.TryGetPrefab(prefabName, out GameObject cityPrefab))
                return null;

            Debug.Log($"<color=#00fffb>SpawningSystem</color> - place <b>{prefabName}</b> on tile {tile.Position}");

            Instantiate(
                cityPrefab,
                tile.Position + new float3(0, 0.1f, 0),
                Quaternion.Euler(90, 90 * Random.Range(0, 3), 0),
                _placablesParent);

            tile.AddUrbanization(50);

            return null;
        }

    }
}
