using System;

namespace Assets._Project._Scripts.UI.Managers.Tooltips
{
    public class TooltipManager : UISystem
    {
        public override void HandleEvent(Enum eventType, object eventData)
        {
            if (eventType is not TooltipEvent tooltipEvent)
                return;
        }
    }
}