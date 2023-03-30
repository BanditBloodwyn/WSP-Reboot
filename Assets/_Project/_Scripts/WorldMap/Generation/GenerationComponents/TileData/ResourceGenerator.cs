using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.TileData
{
    [CreateAssetMenu(fileName = "ResourceGenerator", menuName = "ScriptableObjects/Settings/World/Generators/Resource Generator")]
    public class ResourceGenerator : ScriptableObject
    {
        public ResourceValues Generate(TilePropertiesComponentData data)
        {
            return default;
        }
    }
}