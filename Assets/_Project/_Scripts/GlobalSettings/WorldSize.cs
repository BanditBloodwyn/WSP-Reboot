using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.GlobalSettings
{
    [CreateAssetMenu(fileName = "WorldSize", menuName = "ScriptableObjects/Settings/World Size")]
    public class WorldSize : ScriptableObject
    {
        [PropertyTooltip("How many chunks in every direction will be spawned.")]
        [Range(1, 32)]
        public int ChunkCountPerAxis;

        [PropertyTooltip("How many tiles in every direction a chunk will contain.")]
        [Range(1, 64)]
        public int TileAmountPerAxis;

    }
}
