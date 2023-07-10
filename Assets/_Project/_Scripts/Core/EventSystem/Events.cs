namespace Assets._Project._Scripts.Core.EventSystem
{
    public static class Events
    {
        public static readonly GameEvent OnTileSelected = new();
        public static readonly GameEvent OnAskForTileSelectionPopupContent = new();

        public static readonly GameEvent OnRequestBuildTemplateBuilding = new();
        
        public static readonly GameEvent OnCreateCivilization = new();
    }
}