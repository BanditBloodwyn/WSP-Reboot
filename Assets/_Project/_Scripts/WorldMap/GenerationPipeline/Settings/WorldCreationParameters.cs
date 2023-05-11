using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings
{
    [CreateAssetMenu(fileName = "WorldCreationParameters", menuName = "ScriptableObjects/Settings/World/World Creation Parameters")]
    public class WorldCreationParameters : ScriptableObject
    {
        [Title("")]
        [PropertyTooltip("How many chunks in every direction will be spawned.")]
        [Range(1, 32)]
        public int ChunkCountPerAxis;

        [PropertyTooltip("How many tiles in every direction a chunk will contain.")]
        [Range(1, 64)]
        public int TileAmountPerAxis;

        [PropertyTooltip("The transform in the hierarchy all chunk-GameObjects will be spawned in.")]
        public Transform ChunkParent;

        public Material DefaultWorldMaterial;

        public VegetationZoneSettings VegetationZoneSettings;

        public TextAsset NoiseSettingJSON;
    }
}