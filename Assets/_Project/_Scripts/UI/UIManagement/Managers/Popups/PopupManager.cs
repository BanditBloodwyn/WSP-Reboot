using Assets._Project._Scripts.Core.Data.Types;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using System.Collections.Generic;
using Assets._Project._Scripts.UI.UIPrefabs;
using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UIManagement.Managers.Popups
{
    public class PopupManager : Singleton<PopupManager>
    {
        [SerializeField] private Transform _movablePopupParent;
        [SerializeField] private Vector2 _popupOffset;

        private readonly Dictionary<string, Popup> _openPopups = new();

        public void OpenMovablePopup(Component sender, object eventData)
        {
            if (eventData is not IPopupDataContainer dataContainer)
                return;

            if (_openPopups.ContainsKey(dataContainer.ContentIdentifier))
                return;

            if (!UIPrefabs.UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.MovablePopup, out GameObject prefab))
                return;

            GameObject popup = Instantiate(prefab, _movablePopupParent);
            popup.transform.position += _openPopups.Count * new Vector3(_popupOffset.x, _popupOffset.y, 0);

            Popup component = popup.GetComponent<Popup>();
            component.ApplyData(dataContainer);
            component.ClosePopup.AddListener(delegate { ClosePopup(component); });

            _openPopups.Add(component.ContentIdentifier, component);
        }

        public void ClosePopup(object eventData)
        {
            if (eventData is not Popup popupToClose)
                return;

            Destroy(popupToClose.gameObject);
            _openPopups.Remove(popupToClose.ContentIdentifier);
        }
    }
}