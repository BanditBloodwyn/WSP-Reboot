using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;
using UnityEngine;

namespace Assets._Project._Scripts.CoreFeatures.MapModes
{
    [CreateAssetMenu(fileName = "MapMode", menuName = "ScriptableObjects/Settings/UI/Map Mode")]
    public class MapModeSO : ScriptableObject
    {
        public string DisplayName;
        public string Description;
        public MapModeCategory Category;
        public Gradient Colors;
        public Sprite UIIcon;
        public Material WorldMapMaterial;
        public bool CalculateShaderBuffer;
        public TileProperties Property;
        public bool DrawWaterAsBlue;
        public Color WaterColor;
    }
}