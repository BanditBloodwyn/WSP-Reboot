using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.Core.Enum.PrefabLists;
using Assets._Project._Scripts.Core.Types;
using UnityEngine;

namespace Assets._Project._Scripts.UI
{
    public class UIPrefabs : PersistentSingleton<UIPrefabs>
    {
        [SerializeField] private List<PrefabCatalogEntry> _catalog;

        public bool TryGetPrefab(UIPrefabNames prefabName, out GameObject prefab)
        {
            if (_catalog.All(entry => entry.Name != prefabName))
            {
                prefab = null;
                return false;
            }

            prefab = _catalog.First(entry => entry.Name == prefabName).Prefab;
            return true;
        }
    }
}