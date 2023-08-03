using Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Settings.Scriptables;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.Container;
using Assets._Project._Scripts.WorldMap.WorldMapCreation2.Data.DataComponents;
using Unity.Collections;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation2.Helper
{
    public static class WorldCreationParametersCreator
    {
        public static WorldCreationParametersComponent Create(WorldCreationParameters parameters)
        {
            WorldCreationParametersComponent component = new()
            {
                WorldSize = CreateWorldSize(parameters),
                NoiseFilters = CreateNoiseFilters(parameters)
            };

            return component;
        }

        private static WorldSizeContainer CreateWorldSize(WorldCreationParameters parameters)
        {
            return new WorldSizeContainer
            {
                ChunkCountPerAxis = parameters.WorldSize.ChunkCountPerAxis,
                TileAmountPerAxis = parameters.WorldSize.TileAmountPerAxis
            };
        }

        private static NativeArray<NoiseFilter> CreateNoiseFilters(WorldCreationParameters parameters)
        {
            return new NativeArray<NoiseFilter>(new[]
                {
                    new StandardNoiseFilter
                    {
                        NumberOfLayers = 6,
                        MinValue = 2.7561436f,
                        MaxValue = 20f,
                        Strength = 3,
                        BaseRoughness = 0.0014744801f,
                        Roughness = 1,
                        Persistance = 0.4725898f
                    }.ToNoiseFilter(),
                    new RigidNoiseFilter
                    {
                        NumberOfLayers = 6,
                        MinValue = 0,
                        MaxValue = 20f,
                        Strength = 5.0888467f,
                        BaseRoughness = 0.0021739132f,
                        Roughness = 2.3988657f,
                        Persistance = 1.8279773f,
                        WeightMultiplier = 0.51039696f
                    }.ToNoiseFilter(),
                    new StandardNoiseFilter
                    {
                        NumberOfLayers = 8,
                        MinValue = 0,
                        MaxValue = 20f,
                        Strength = 0.60491496f,
                        BaseRoughness = 0.0025330812f,
                        Roughness = 0.6143667f,
                        Persistance = 0.4725898f
                    }.ToNoiseFilter(),
                },
                Allocator.Persistent);
        }
    }
}