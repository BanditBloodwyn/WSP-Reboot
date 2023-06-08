using AdvancedNoiseLib.Math.Noise.Filter;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables
{
    [CreateAssetMenu(fileName = "NoiseParameters", menuName = "ScriptableObjects/Settings/World/Noise Parameters")]
    public class NoiseParameters : ScriptableObject
    {
        [SerializeReference] public INoiseFilter[] NoiseFilters;
    }
}