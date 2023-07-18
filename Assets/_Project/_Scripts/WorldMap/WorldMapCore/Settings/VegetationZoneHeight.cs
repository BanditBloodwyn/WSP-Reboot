using System;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Enums;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.Settings
{
    [Serializable]
    public struct VegetationZoneHeight
    {
        public VegetationZones VegetationZone;
        public float MaximumHeight;
    }
}