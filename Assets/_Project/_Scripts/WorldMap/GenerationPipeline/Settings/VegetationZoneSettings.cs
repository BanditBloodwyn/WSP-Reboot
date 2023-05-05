using Assets._Project._Scripts.WorldMap.Data.Structs;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings
{
    [CreateAssetMenu(fileName = "VegetationZoneSettings", menuName = "ScriptableObjects/Settings/World/Vegetation Zone Settings")]
    public class VegetationZoneSettings : ScriptableObject
    {
        [SerializeField] public VegetationZoneHeight[] VegetationZones;
    }
}