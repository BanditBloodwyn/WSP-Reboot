﻿using System;

namespace Assets._Project._Scripts.UI.Managers.HUD
{
    public class HUDManager : UISystem
    {
        public override void HandleEvent(Enum eventType, object eventData)
        {
            if (eventType is not HUDEvent hudEvent)
                return;
        }
    }
}