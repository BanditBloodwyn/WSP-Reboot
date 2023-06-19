using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using Assets._Project._Scripts.UI.UIPrefabs;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._Scripts.Features.ObjectPlacementSystem.UI
{
    public class BuildMenuUIData : IPopupDataContainer
    {
        private readonly TileAspect _tile;

        public BuildMenuUIData(TileAspect tile)
        {
            _tile = tile;
        }

        public string Title => "Building";

        public void ApplyContent(Transform contentPanel)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.TextButton, out GameObject textButtonPrefab))
                return;
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanelSeperator, out GameObject seperatorPrefab))
                return;

            Transform buildTab = contentPanel.transform;

            GameObject buttonObject = Object.Instantiate(textButtonPrefab, buildTab);
            buttonObject.GetComponentInChildren<TMP_Text>().text = "Build";
            buttonObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { OnClickBuild(buttonObject); });

            Object.Instantiate(seperatorPrefab, buildTab);
        }

        private void OnClickBuild(GameObject buttonObject)
        {
            Events.OnRequestBuildTemplateBuilding.Invoke(buttonObject.transform, _tile);
        }
    }
}