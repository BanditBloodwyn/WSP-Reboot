using Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters;
using Unity.Collections;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.Container.Noise
{
    public struct NoiseFilterSettingsContainer
    {
        public NativeArray<StandardNoiseFilter> StandardNoiseFilters;
        public NativeArray<RigidNoiseFilter> RigidNoiseFilters;
    }
}