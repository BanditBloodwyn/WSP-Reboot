using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.Settings.Scriptables
{
    [CreateAssetMenu(fileName = "VegetationZoneSettings", menuName = "ScriptableObjects/Settings/World/Vegetation Zone Settings")]
    public class VegetationZoneSettings : ScriptableObject
    {
        [SerializeField] public VegetationZoneHeight[] VegetationZones;
    }
}