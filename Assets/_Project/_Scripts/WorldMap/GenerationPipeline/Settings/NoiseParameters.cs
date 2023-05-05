using Assets._Project._Scripts.Core.Math.Noise.NoiseFilters;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings
{
    [CreateAssetMenu(fileName = "NoiseParameters", menuName = "ScriptableObjects/Settings/World/Noise Parameters")]
    public class NoiseParameters : ScriptableObject
    {
        [SerializeReference] public INoiseFilter[] NoiseFilters;
    }
}