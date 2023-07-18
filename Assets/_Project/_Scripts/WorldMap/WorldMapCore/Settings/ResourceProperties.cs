using System;
using Assets._Project._Scripts.Core.Data.Math.Noise.NoiseFilters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.Settings
{
    [Serializable]
    public struct ResourceProperties
    {
        public string ResourceName;
        public AnimationCurve Distribution;

        [Title("Noise")]
        [HideInInspector] public int Seed;
        public StandardNoiseFilter NoiseFilter;
    }
}