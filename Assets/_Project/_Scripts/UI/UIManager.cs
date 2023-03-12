using System.Collections.Generic;
using Assets._Project._Scripts.Core.Enum.PrefabLists;
using Assets._Project._Scripts.Core.Types;
using Assets._Project._Scripts.UI.Controls;
using Assets._Project._Scripts.UI.Core;
using UnityEngine;

namespace Assets._Project._Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Transform _movablePopupParent;
        [SerializeField] private Vector2 _popupOffset;

        private readonly Dictionary<string, Popup> _openPopups = new();

        public void OpenMovablePopup(IUIDataContainer data)
        {
            if (_openPopups.ContainsKey(data.ContentIdentifier))
                return;

            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.MovablePopup, out GameObject prefab))
                return;
            
            GameObject popup = Instantiate(prefab, _movablePopupParent);
            popup.transform.position += _openPopups.Count * new Vector3(_popupOffset.x, _popupOffset.y, 0);
            
            Popup component = popup.GetComponent<Popup>();
            component.ApplyData(data);

            _openPopups.Add(component.ContentIdentifier, component);
        }

        public void ClosePopup(Popup popupToClose)
        {
            Destroy(popupToClose.gameObject);
            _openPopups.Remove(popupToClose.ContentIdentifier);
        }
    }
}
