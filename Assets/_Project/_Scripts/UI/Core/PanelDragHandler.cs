using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Project._Scripts.UI.Core
{
    public class PanelDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] private RectTransform _panelToMove;
        private Vector2 _offset;

        public void OnDrag(PointerEventData eventData)
        {
            _panelToMove.anchoredPosition = eventData.position - _offset; 
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _offset = eventData.position - _panelToMove.anchoredPosition;
        }
    }
}
