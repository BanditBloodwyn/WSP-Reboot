using System;
using Assets._Project._Scripts.WorldMap.Data.Enums;

namespace Assets._Project._Scripts.WorldMap.Data.Structs
{
    [Serializable]
    public struct VegetationZoneHeight
    {
        public VegetationZones VegetationZone;
        public float MaximumHeight;
    }
}