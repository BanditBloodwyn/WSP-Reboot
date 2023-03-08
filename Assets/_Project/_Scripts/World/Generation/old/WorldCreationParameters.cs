using Assets._Project._Scripts.World.Data.Structs;
using Assets._Project._Scripts.World.Generation.Math.NoiseFilters;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.old
{
    [CreateAssetMenu(fileName = "WorldCreationParameters", menuName = "ScriptableObjects/Settings/World/World Creation Parameters")]
    public class WorldCreationParameters : ScriptableObject
    {
        public bool GenerateHeights;

        [Title("")]
        [PropertyTooltip("How many chunks in every direction will be spawned.")]
        [Range(1, 32)]
        public int WorldSize;

        [PropertyTooltip("How many tiles in every direction a chunk will contain.")]
        [Range(1, 64)]
        public int ChunkSize;

        [SerializeReference]
        public INoiseFilter[] NoiseFilters;

        public VegetationZoneHeight[] vegetationZoneHeights;
    }
}