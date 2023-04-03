using Assets._Project._Scripts.UI.MapModesUI.MapModes;
using NUnit.Framework;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Systems.MapModes
{
    public class MapModeSystem : MonoBehaviour
    {
        [SerializeField] private Transform _chunkParent;

        private void Awake()
        {
            Assert.IsNotNull(_chunkParent);
        }

        public void ChangeMapMode(MapMode mapMode)
        {
            if(mapMode.WorldMapMaterial == null)
            {
                Debug.LogWarning($"No material set for map mode \"<color=#73BD73>{mapMode.DisplayName}</color>\"");
                return;
            }

            for (int i = 0; i < _chunkParent.childCount; i++)
                _chunkParent.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = mapMode.WorldMapMaterial;

            Debug.Log($"Changed map mode to \"<color=#73BD73>{mapMode.DisplayName}</color>\"");
        }
    }
}
