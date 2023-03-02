using System;
using Assets._Project._Scripts.World.Data.Enums;

namespace Assets._Project._Scripts.World.Data.Structs
{
    [Serializable]
    public struct VegetationZoneHeight
    {
        public VegetationZones VegetationZone;
        public float MaximumHeight;
    }
}