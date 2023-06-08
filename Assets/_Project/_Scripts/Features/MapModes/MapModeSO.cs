using Assets._Project._Scripts.Features.WorldMap.WorldMapCore.Enums;
using Assets._Project._Scripts.UI.UICore.Interfaces;
using UnityEngine;

namespace Assets._Project._Scripts.Features.MapModes
{
    [CreateAssetMenu(fileName = "MapMode", menuName = "ScriptableObjects/Settings/UI/Map Mode")]
    public class MapModeSO : ScriptableObject, IButtonCreatable
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

        public string ButtonName => DisplayName;
        public Sprite ButtonIcon => UIIcon;
    }
}