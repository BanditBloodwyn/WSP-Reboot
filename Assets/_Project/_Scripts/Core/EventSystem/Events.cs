namespace Assets._Project._Scripts.Core.EventSystem
{
    public static class Events
    {
        public static readonly GameEvent OnStartWorldCreation = new("OnStartWorldCreation");
        public static readonly GameEvent OnWorldCreationFinished = new("OnWorldCreationFinished");

        public static readonly GameEvent OnRequestSwitchMapMode = new("OnRequestSwitchMapMode");
        public static readonly GameEvent OnMapModeChosen = new("OnMapModeChosen");

        public static readonly GameEvent OnTileSelected = new("OnTileSelected");
        public static readonly GameEvent OnAskForTileSelectionPopupContent = new("OnAskForTileSelectionPopupContent");
        public static readonly GameEvent OnCloseTileSelectionPopup = new("OnCloseTileSelectionPopup");

        public static readonly GameEvent OnRequestBuildTemplateBuilding = new("OnRequestBuildTemplateBuilding");
        
        public static readonly GameEvent OnCreateCivilization = new("OnCreateCivilization");
    }
}