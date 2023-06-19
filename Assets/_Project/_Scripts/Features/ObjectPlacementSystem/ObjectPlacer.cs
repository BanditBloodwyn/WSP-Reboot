using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using UnityEngine;

namespace Assets._Project._Scripts.Features.ObjectPlacementSystem
{
    public class ObjectPlacer : MonoBehaviour
    {
        #region Unity

        private void OnEnable()
        {
            Events.OnRequestBuildTemplateBuilding.AddListener(Place);
        }

        private void OnDisable()
        {
            Events.OnRequestBuildTemplateBuilding.RemoveListener(Place);
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        #endregion


        private object Place(Component sender, object data)
        {
            if (data is not TileAspect tile)
                return null;

            Debug.Log($"<color=#00fffb>SpawningSystem</color> - place on tile {tile.Position}");

            return null;
        }

    }
}
