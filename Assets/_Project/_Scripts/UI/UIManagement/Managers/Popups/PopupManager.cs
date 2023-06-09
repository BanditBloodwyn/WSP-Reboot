using System;
using System.Collections.Generic;
using Assets._Project._Scripts.UI.UICore.Controls;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using Assets._Project._Scripts.UI.UIManagement.Prefabs;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.UI.UIManagement.Managers.Popups
{
    public class PopupManager : UISystem
    {
        [SerializeField] private Transform _movablePopupParent;
        [SerializeField] private Vector2 _popupOffset;

        private readonly Dictionary<string, Popup> _openPopups = new();

        protected override void Awake()
        {
            base.Awake();

            Assert.IsNotNull(_movablePopupParent);
        }

        public override void HandleEvent(Enum eventType, object eventData)
        {
            if (eventType is not PopupEvent popupEvent)
                return;

            switch (popupEvent)
            {
                case PopupEvent.OpenMovablePopup:
                    OpenMovablePopup(eventData);
                    break;
                case PopupEvent.ClosePopup:
                    ClosePopup(eventData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OpenMovablePopup(object eventData)
        {
            if (eventData is not IPopupDataContainer dataContainer)
                return;

            if (_openPopups.ContainsKey(dataContainer.ContentIdentifier))
                return;

            if (!UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.MovablePopup, out GameObject prefab))
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