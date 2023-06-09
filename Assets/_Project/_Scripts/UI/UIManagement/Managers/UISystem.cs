using Assets._Project._Scripts.Core.Data.Types;
using Assets._Project._Scripts.UI.UIManagement.Managers.HUD;
using System;

namespace Assets._Project._Scripts.UI.UIManagement.Managers
{
    public abstract class UISystem : Singleton<HUDManager>
    {
        public abstract void HandleEvent(Enum eventType, object eventData);
    }
}