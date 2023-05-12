using Assets._Project._Scripts.WorldMap.Data.Structs;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.Settings
{
    [CreateAssetMenu(fileName = "ResourceSettings", menuName = "ScriptableObjects/Settings/World/Resource Settings")]
    public class ResourceSettings : ScriptableObject
    {
        public ResourceProperties[] ResourceProperties;
        
        public void Init()
        {
            for (int index = 0; index < ResourceProperties.Length; index++)
            {
                ResourceProperties resourceProperty = ResourceProperties[index];
                resourceProperty.Seed = (int)(Random.value * int.MaxValue);
                ResourceProperties[index] = resourceProperty;
            }
        }
    }
}