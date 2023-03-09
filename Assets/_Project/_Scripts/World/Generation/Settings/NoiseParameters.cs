using Assets._Project._Scripts.World.Generation.GenerationComponents.Terrain.Math.NoiseFilters;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.Settings
{
    [CreateAssetMenu(fileName = "NoiseParameters", menuName = "ScriptableObjects/Settings/World/Noise Parameters")]
    public class NoiseParameters : ScriptableObject
    {
        [SerializeReference] public INoiseFilter[] NoiseFilters;
    }
}