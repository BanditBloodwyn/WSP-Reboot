using Assets._Project._Scripts.Core.Enum.PrefabLists;
using Assets._Project._Scripts.UI.Controls;
using Assets._Project._Scripts.UI.Core;
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
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanel, out GameObject prefab))
                return;
            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.EntryPanelSeperator, out GameObject seperator))
                return;

            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Vegetation zone", _tile.GetVegetationZone());
            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Height", _tile.Position.y.ToString("F2"));
            
            Object.Instantiate(seperator, contentPanel.transform);

            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Deciduous trees", _tile.GetDeciduousTrees().ToString("F2"));
            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Evergreen trees", _tile.GetEvergreenTrees().ToString("F2"));
            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Herbs", _tile.GetHerbs().ToString("F2"));
            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Fruits", _tile.GetFruits().ToString("F2"));
           
            Object.Instantiate(seperator, contentPanel.transform);

            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Oil", _tile.GetOil().ToString("F2"));
            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Gas", _tile.GetGas().ToString("F2"));
        }
    }
}