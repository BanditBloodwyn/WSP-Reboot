using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.Features.SelectionSystem.Settings
{
    [CreateAssetMenu(fileName = "TileSelectionSystemSettings", menuName = "ScriptableObjects/Settings/Systems/Tile Selection/Settings")]
    public class TileSelectionSystemSettings : ScriptableObject
    {
        public GameObject SelectorPrefab;
        public Color SelectorTint;

        public bool RealTimeSelectorMovement;
        
        [Title("")]
        [Range(0, 0.5f)] public float HighlightFadeDuration = 0.1f;
    }
}