using Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.Container;
using Unity.Collections;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents
{
    public struct WorldCreationParametersComponent : IComponentData
    {
        public WorldSizeContainer WorldSize;
        [ReadOnly] public NativeArray<NoiseFilter> NoiseFilters;
    }
}