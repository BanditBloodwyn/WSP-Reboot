using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.TileData
{
    [CreateAssetMenu(fileName = "FaunaGenerator", menuName = "ScriptableObjects/Settings/World/Generators/Fauna Generator")]
    public class FaunaGenerator : ScriptableObject
    {
        public FaunaValues Generate(TilePropertiesComponentData data)
        {
            return default;
        }
    }
}