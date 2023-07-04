using Assets._Project._Scripts.UI.UICore.Interfaces;
using Assets._Project._Scripts.UI.UIPrefabs;
using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using UnityEngine;

namespace Assets._Project._Scripts.CoreFeatures.SelectionSystem.UI
{
    public class TileSelectionUIData : IPopupDataContainer
    {
        private readonly TileAspect _tile;

        public string Title => "Details";

        public TileSelectionUIData(TileAspect tile)
        {
            _tile = tile;
        }

        public void ApplyContent(Transform contentPanel)
        {
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanel, out GameObject entryPanelPrefab))
                return;
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanelSeperator, out GameObject seperatorPrefab))
                return;

            Transform detailsTab = contentPanel.transform;

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