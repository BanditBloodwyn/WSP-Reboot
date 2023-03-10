using Assets._Project._Scripts.Core.Enum.PrefabLists;
using Assets._Project._Scripts.UI.Controls;
using Assets._Project._Scripts.UI.Core;
using Assets._Project._Scripts.World.ECS.Aspects;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.DataContainer
{
    public class TileSelectionDataContainer : IUIDataContainer
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

            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Vegetation zone", _tile.GetVegetationZone());
            Object.Instantiate(prefab, contentPanel.transform).GetComponent<EntryPanel>().Set("Height", _tile.Position.y.ToString("F2"));
        }
    }
}