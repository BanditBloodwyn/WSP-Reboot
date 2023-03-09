using Assets._Project._Scripts.World.Generation.GenerationComponents;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.Settings
{
    [CreateAssetMenu(fileName = "WorldCreationParameters", menuName = "ScriptableObjects/Settings/World/World Creation Parameters")]
    public class WorldCreationParameters : ScriptableObject
    {
        [Title("")]
        [PropertyTooltip("How many chunks in every direction will be spawned.")]
        [Range(1, 32)]
        public int WorldSize;

        [PropertyTooltip("How many tiles in every direction a chunk will contain.")]
        [Range(1, 64)]
        public int ChunkSize;

        [SerializeReference]
        public IGenerationComponent[] GenerationComponents;
    }
}