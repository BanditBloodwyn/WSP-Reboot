using System;
using Assets._Project._Scripts.Core.Types;
using Assets._Project._Scripts.UI.Managers.HUD;

namespace Assets._Project._Scripts.UI.Managers
{
    public abstract class UISystem : Singleton<HUDManager>
    {
        public abstract void HandleEvent(Enum eventType, object eventData);
    }
}