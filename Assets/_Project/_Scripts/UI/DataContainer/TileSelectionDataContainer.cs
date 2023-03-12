using Assets._Project._Scripts.UI.Core;
using Assets._Project._Scripts.World.ECS.Aspects;
using TMPro;
using UnityEngine;

namespace Assets._Project._Scripts.UI.DataContainer
{
    public class TileSelectionDataContainer : IUIDataContainer
    {
        private readonly TileAspect _tile;

        public TileSelectionDataContainer(TileAspect tile)
        {
            _tile = tile;
        }

        public void ApplyHeader(TMP_Text tmpText)
        {
            tmpText.text = $"Tile - {_tile.Position.x}, {_tile.Position.z}";
        }

        public void ApplyContent(GameObject gameObject)
        {

        }
    }
}