using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Systems.TileSelection.Settings
{
    [CreateAssetMenu(fileName = "TileSelectionSystemSettings", menuName = "ScriptableObjects/Settings/Systems/Tile Selection/Settings")]
    public class TileSelectionSystemSettings : ScriptableObject
    {
        public GameObject SelectorPrefab;
        public Color SelectorTint;
        
        [Title("")]
        [Range(0, 0.5f)] public float HighlightFadeDuration = 0.1f;
    }
}