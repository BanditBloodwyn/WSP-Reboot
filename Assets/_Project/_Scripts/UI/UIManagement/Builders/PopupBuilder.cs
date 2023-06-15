using System;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using Assets._Project._Scripts.UI.UIPrefabs;
using Assets._Project._Scripts.UI.UIPrefabs.Controls;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project._Scripts.UI.UIManagement.Builders
{
    public class PopupBuilder
    {
        private Transform _parent;
        private string _title;
        private readonly List<IPopupDataContainer> _tabContent = new();

        public PopupBuilder SetParent(Transform parent)
        {
            _parent = parent;
            return this;
        }

        public PopupBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public PopupBuilder AddTabContent(object[] tabContent)
        {
            if (tabContent.Any(static d => d is not IPopupDataContainer))
                return null;

            _tabContent.AddRange(Array.ConvertAll(
                tabContent, 
                static input => input as IPopupDataContainer));

            return this;
        }

        public Popup Build()
        {
            if (!UIPrefabs.UIPrefabs.Instance.TryGetPrefab(UIPrefabNames.MovablePopup, out GameObject prefab))
                return null;

            if (_parent == null)
                return null;

            GameObject popupObject = Object.Instantiate(prefab, _parent);
            Popup popup = popupObject.GetComponent<Popup>();
            
            popup.SetTitle(_title);
            popup.ApplyData(_tabContent.ToArray());

            return popup;
        }
    }
}