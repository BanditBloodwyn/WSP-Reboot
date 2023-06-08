using Assets._Project._Scripts.Features.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.UI.Prefabs;
using Assets._Project._Scripts.UI.UICore.Controls;
using Assets._Project._Scripts.UI.UICore.Controls.TabControl;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.Features.SelectionSystem.UI
{
    public class TileSelectionPopupDataContainer : IPopupDataContainer
    {
        private readonly TileAspect _tile;

        public string ContentIdentifier => $"Tile - {_tile.Position.x}, {_tile.Position.z}";

        public TileSelectionPopupDataContainer(TileAspect tile)
        {
            _tile = tile;
        }

        public void ApplyHeader(TMP_Text header)
        {
            header.text = ContentIdentifier;
        }

        public void ApplyContent(Transform contentPanel)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.TabControl, out GameObject tabControlPrefab))
                return;
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanel, out GameObject entryPanelPrefab))
                return;
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanelSeperator, out GameObject seperatorPrefab))
                return;

            GameObject tabControlInstance = Object.Instantiate(tabControlPrefab, contentPanel.transform);
            TabGroup tabGroup = tabControlInstance.GetComponentInChildren<TabGroup>();

            BuildOverviewTab(tabGroup, seperatorPrefab);
            BuildBuildingsTab(tabGroup, seperatorPrefab);
            BuildDetailsTab(tabGroup, entryPanelPrefab, seperatorPrefab);

            tabGroup.SelectTabByIndex(0);
        }

        private void BuildOverviewTab(TabGroup tabGroup, GameObject seperatorPrefab)
        {
            tabGroup.SetTabHeader(0, "Overview");

        }

        private void BuildBuildingsTab(TabGroup tabGroup, GameObject seperatorPrefab)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.TextButton, out GameObject textButtonPrefab))
                return;

            tabGroup.SetTabHeader(1, "Buildings");

            Transform buildingsTab = tabGroup.GetTabPageTransform(1);

            Object.Instantiate(textButtonPrefab, buildingsTab).GetComponentInChildren<TMP_Text>().text = "Build";
            Object.Instantiate(seperatorPrefab, buildingsTab);
        }

        private void BuildDetailsTab(TabGroup tabGroup, GameObject entryPanelPrefab, GameObject seperatorPrefab)
        {
            tabGroup.SetTabHeader(2, "Info");

            Transform detailsTab = tabGroup.GetTabPageTransform(2);

            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Vegetation zone", _tile.GetVegetationZone());
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Height", _tile.Position.y.ToString("F2"));

            Object.Instantiate(seperatorPrefab, detailsTab);

            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Deciduous trees", _tile.GetDeciduousTrees().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Evergreen trees", _tile.GetEvergreenTrees().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Herbs", _tile.GetHerbs().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Fruits", _tile.GetFruits().ToString("F2"));

            Object.Instantiate(seperatorPrefab, detailsTab);

            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Herbivores", _tile.GetHerbivores().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Carnivores", _tile.GetCarnivores().ToString("F2"));

            Object.Instantiate(seperatorPrefab, detailsTab);

            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Oil", _tile.GetOil().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Gas", _tile.GetGas().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Coal", _tile.GetCoal().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Iron", _tile.GetIron().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Gold", _tile.GetGold().ToString("F2"));

            Object.Instantiate(seperatorPrefab, detailsTab);

            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Urbanization", _tile.GetUrbanization().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, detailsTab).GetComponent<EntryPanel>().Set("Life standard", _tile.GetLifeStandard().ToString("F2"));
        }
    }
}