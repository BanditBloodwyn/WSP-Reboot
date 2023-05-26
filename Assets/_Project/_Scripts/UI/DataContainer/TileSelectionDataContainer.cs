using Assets._Project._Scripts.Core.Enum.PrefabLists;
using Assets._Project._Scripts.UI.Controls;
using Assets._Project._Scripts.UI.Core;
using Assets._Project._Scripts.UI.Core.TabControl;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.DataContainer
{
    public class TileSelectionDataContainer : IPopupDataContainer
    {
        private readonly TileAspect _tile;

        public string ContentIdentifier => $"Tile - {_tile.Position.x}, {_tile.Position.z}";

        public TileSelectionDataContainer(TileAspect tile)
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

            BuildInfoTab(tabGroup, entryPanelPrefab, seperatorPrefab);
            BuildInteractionTab(tabGroup, entryPanelPrefab, seperatorPrefab);
            tabGroup.SelectTabByIndex(0);
        }

        private void BuildInfoTab(TabGroup tabGroup, GameObject entryPanelPrefab, GameObject seperatorPrefab)
        {
            tabGroup.SetTabHeader(0, "Info");

            Transform infoTab = tabGroup.GetTabPageTransform(0);

            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Vegetation zone", _tile.GetVegetationZone());
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Height", _tile.Position.y.ToString("F2"));

            Object.Instantiate(seperatorPrefab, infoTab);

            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Deciduous trees", _tile.GetDeciduousTrees().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Evergreen trees", _tile.GetEvergreenTrees().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Herbs", _tile.GetHerbs().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Fruits", _tile.GetFruits().ToString("F2"));

            Object.Instantiate(seperatorPrefab, infoTab);

            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Herbivores", _tile.GetHerbivores().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Carnivores", _tile.GetCarnivores().ToString("F2"));

            Object.Instantiate(seperatorPrefab, infoTab);

            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Oil", _tile.GetOil().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Gas", _tile.GetGas().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Coal", _tile.GetCoal().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Iron", _tile.GetIron().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Gold", _tile.GetGold().ToString("F2"));
           
            Object.Instantiate(seperatorPrefab, infoTab);
           
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Urbanization", _tile.GetUrbanization().ToString("F2"));
            Object.Instantiate(entryPanelPrefab, infoTab).GetComponent<EntryPanel>().Set("Life standard", _tile.GetLifeStandard().ToString("F2"));
        }

        private void BuildInteractionTab(TabGroup tabGroup, GameObject entryPanelPrefab, GameObject seperatorPrefab)
        {
            tabGroup.SetTabHeader(1, "Interaction");
        }
    }
}