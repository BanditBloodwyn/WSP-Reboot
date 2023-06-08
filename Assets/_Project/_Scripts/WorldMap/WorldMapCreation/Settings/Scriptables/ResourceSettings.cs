using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables
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