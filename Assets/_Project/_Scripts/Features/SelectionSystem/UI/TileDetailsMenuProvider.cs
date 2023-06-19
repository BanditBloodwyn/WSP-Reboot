using Assets._Project._Scripts.Core.Data.Interfaces;
using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using UnityEngine;

namespace Assets._Project._Scripts.Features.SelectionSystem.UI
{
    [RequireComponent(typeof(TileSelectionSystem))]
    public class TileDetailsMenuProvider : MonoBehaviour
    {

        private void OnEnable()
        {
            Events.OnAskForTileSelectionPopupContent.AddListener(GetUIData);
        }

        private void OnDisable()
        {
            Events.OnAskForTileSelectionPopupContent.RemoveListener(GetUIData);
        }
       
        private static object GetUIData(Component sender, object eventData)
        {
            return eventData is TileAspect tile 
                ? new TileSelectionUIData(tile) 
                : null;
        }
    }
}
