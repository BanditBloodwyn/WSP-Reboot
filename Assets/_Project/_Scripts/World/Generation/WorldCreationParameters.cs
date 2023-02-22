using System;
using Assets._Project._Scripts.World.Generation.Math.NoiseFilters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    [Serializable]
    public struct WorldCreationParameters
    {
        public int Seed;

        [PropertyTooltip("How many chunks in every direction will be spawned.")]
        [Range(1, 30)]
        public int WorldSize;

        [PropertyTooltip("How many tiles in every direction a chunk will contain.")]
        [Range(1, 30)]
        public int ChunkSize;

        [SerializeReference]
        public INoiseFilter[] NoiseFilters;
    }
}