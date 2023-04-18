using UnityEditor;
using UnityEngine;

namespace Assets._Project._Scripts.Core.Types
{
    public static class ScriptableObjectUtils
    {
        public static T[] FindAssetsOfType<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name); // Find all assets of type T
            T[] results = new T[guids.Length];
           
            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                results[i] = AssetDatabase.LoadAssetAtPath<T>(path); // Load the asset at the path
            }

            return results;
        }
    }
}