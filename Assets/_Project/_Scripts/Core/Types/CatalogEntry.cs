using Assets._Project._Scripts.Core.Enum.PrefabLists;
using System;
using UnityEngine;

namespace Assets._Project._Scripts.Core.Types
{
    [Serializable]
    public struct PrefabCatalogEntry
    {
        public UIPrefabNames Name;
        public GameObject Prefab;
    }
}