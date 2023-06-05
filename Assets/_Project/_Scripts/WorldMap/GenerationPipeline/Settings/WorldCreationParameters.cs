using Assets._Project._Scripts.GlobalSettings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings
{
    [CreateAssetMenu(fileName = "WorldCreationParameters", menuName = "ScriptableObjects/Settings/World/World Creation Parameters")]
    public class WorldCreationParameters : ScriptableObject
    {
        [Title("")]
        public WorldSize WorldSize;

        [Title("")]
        [PropertyTooltip("The transform in the hierarchy all chunk-GameObjects will be spawned in.")]
        public Transform ChunkParent;

        public Material DefaultWorldMaterial;

        [Title("Settings")]
        public VegetationZoneSettings VegetationZoneSettings;

        public ResourceSettings ResourceSettings;

        public TextAsset NoiseSettingJSON;

        public void Init()
        {
            ResourceSettings.Init();
        }
    }
}