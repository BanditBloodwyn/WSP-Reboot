using Assets._Project._Scripts.WorldMap.Data.Enums;
using UnityEngine;

namespace Assets._Project._Scripts.UI.MapModesUI.MapModes
{
    [CreateAssetMenu(fileName = "MapMode", menuName = "ScriptableObjects/Settings/UI/Map Mode")]
    public class MapMode : ScriptableObject
    {
        public string DisplayName;

        public string Description;

        public MapModeCategory Category;

        public Gradient Colors;

        public Sprite UIIcon;

        public Material WorldMapMaterial;

        public bool CalculateShaderBuffer;
       
        public TileProperties Property;
    }
}