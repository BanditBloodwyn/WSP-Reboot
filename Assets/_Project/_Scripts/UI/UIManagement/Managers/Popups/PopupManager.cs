using Assets._Project._Scripts.Core.Data.Interfaces;
using Assets._Project._Scripts.Core.Data.Types;
using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.UI.UIManagement.Builders;
using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UIManagement.Managers.Popups
{
    public class PopupManager : Singleton<PopupManager>
    {
        [SerializeField] private Transform _movablePopupParent;
        [SerializeField] private Vector2 _popupOffset;

        private readonly Dictionary<string, Popup> _openPopups = new();

        #region Unity

        private void OnEnable()
        {
            Events.OnTileSelected.AddListener(OpenMovablePopup);
        }

        private void OnDisable()
        {
            Events.OnTileSelected.RemoveListener(OpenMovablePopup);
        }

        #endregion

        private object OpenMovablePopup(Component sender, object eventData)
        {
            if (eventData is not INamedObject namedObject)
                return null;

            if (_openPopups.ContainsKey(namedObject.Name))
                return null;

            Popup popup = CreatePopup(eventData, namedObject.Name);
            if (popup == null)
                return null;

            _openPopups.Add(namedObject.Name, popup);

            return null;
        }

        private Popup CreatePopup(object content, string title)
        {
            Popup popup = new PopupBuilder()
                .SetParent(_movablePopupParent)
                .SetTitle(title)
                .AddTabContent(Events.OnAskForTileSelectionPopupContent.Invoke(this, content))
                .Build();

            if (popup == null)
                return null;

            Events.OnCloseTileSelectionPopup.AddListener(ClosePopup);
            popup.transform.position += _openPopups.Count * new Vector3(_popupOffset.x, _popupOffset.y, 0);

            return popup;
        }

        private object ClosePopup(Component sender, object data)
        {
            if (!sender.TryGetComponent(out Popup popupToClose))
                return null;

            Destroy(popupToClose.gameObject);
            _openPopups.Remove(popupToClose.ContentIdentifier);

            return null;
        }
    }
}