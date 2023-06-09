using System;
using System.Collections.Generic;
using Assets._Project._Scripts.Core.Types;
using UnityEngine;

namespace Assets._Project._Scripts.UI.UIManagement.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private List<UISystem> uiSystems;

        public void RaiseEvent(Enum eventName, object eventData)
        {
            foreach (UISystem system in uiSystems)
                system.HandleEvent(eventName, eventData);
        }
    }
}
