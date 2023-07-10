using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using Assets._Project._Scripts.UI.UIPrefabs;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project._Scripts.Civilizations.Features.Civilizations.Spawning.UI
{
    public class CivilizationSpawningUIData : IPopupDataContainer
    {
        private readonly TileAspect _tile;

        public string Title => "Civilization";

        public CivilizationSpawningUIData(TileAspect tile)
        {
            _tile = tile;
        }

        public void ApplyContent(Transform contentPanel)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.TextButton, out GameObject textButtonPrefab))
                return;
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanelSeperator, out GameObject seperatorPrefab))
                return;

            Transform buildTab = contentPanel.transform;

            GameObject buttonObject = Object.Instantiate(textButtonPrefab, buildTab);
            buttonObject.GetComponentInChildren<TMP_Text>().text = "Create";
            buttonObject.GetComponentInChildren<Button>().onClick.AddListener(delegate { OnClickBuild(buttonObject); });

            Object.Instantiate(seperatorPrefab, buildTab);
        }

        private void OnClickBuild(GameObject buttonObject)
        {
            Events.OnCreateCivilization.Invoke(buttonObject.transform, _tile);
        }
    }
}