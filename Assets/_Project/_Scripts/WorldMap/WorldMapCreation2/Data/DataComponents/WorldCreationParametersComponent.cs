using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.Container;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.Container.Noise;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents
{
    public struct WorldCreationParametersComponent : IComponentData
    {
        public WorldSizeContainer WorldSize;
        public NoiseFilterSettingsContainer NoiseSettings;
    }
}