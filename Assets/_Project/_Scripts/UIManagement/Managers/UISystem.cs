using System;
using Assets._Project._Scripts.Core.Types;
using Assets._Project._Scripts.UIManagement.Managers.HUD;

namespace Assets._Project._Scripts.UIManagement.Managers
{
    public abstract class UISystem : Singleton<HUDManager>
    {
        public abstract void HandleEvent(Enum eventType, object eventData);
    }
}