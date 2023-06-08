using System;

namespace Assets._Project._Scripts.UIManagement.Managers.Menus
{
    public class MenuManager : UISystem
    {
        public override void HandleEvent(Enum eventType, object eventData)
        {
            if (eventType is not MenuEvent menuEvent)
                return;
        }
    }
}