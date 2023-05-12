using Assets._Project._Scripts.Core.Types;
using Assets._Project._Scripts.UI.Controls;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.UI.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Transform _movablePopupParent;
        [SerializeField] private Vector2 _popupOffset;

        private readonly Dictionary<string, Popup> _openPopups = new();

        [SerializeField] private List<UISystem> uiSystems;

        public void RaiseEvent(Enum eventName, object eventData)
        {
            foreach (UISystem system in uiSystems)
                system.HandleEvent(eventName, eventData);
        }
    }
}
