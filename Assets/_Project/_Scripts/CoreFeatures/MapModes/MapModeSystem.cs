using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.GlobalSettings;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Structs;
using Assets._Project._Scripts.WorldMap.WorldMapManagement;
using NUnit.Framework;
using UnityEngine;

namespace Assets._Project._Scripts.CoreFeatures.MapModes
{
    public class MapModeSystem : MonoBehaviour
    {
        [SerializeField] private WorldSize _worldSize;
        [SerializeField] private Transform _chunkParent;


        #region Unity

        private void OnEnable()
        {
            Events.OnRequestSwitchMapMode.AddListener(ChangeMapMode);
        }

        private void OnDisable()
        {
            Events.OnRequestSwitchMapMode.RemoveListener(ChangeMapMode);
        }

        private void Awake()
        {
            Assert.IsNotNull(_worldSize);
            Assert.IsNotNull(_chunkParent);
        }

        #endregion


        private object ChangeMapMode(Component component, object data)
        {
            if (data is not MapModeSO mapMode)
                return null;

            if (mapMode.WorldMapMaterial == null)
            {
                Debug.LogError($"<color=#73BD73>Map mode system</color> - No material set for map mode \"<color=#73BD73>{mapMode.DisplayName}</color>\"");
                return null;
            }

            if (mapMode.CalculateShaderBuffer)
                CalculateMaterialBuffer(mapMode);

            for (int i = 0; i < _chunkParent.childCount; i++)
                _chunkParent.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = mapMode.WorldMapMaterial;

            Events.OnMapModeChosen.Invoke(this, null);

            return null;
        }

        private void CalculateMaterialBuffer(MapModeSO mapMode)
        {
            TileValue[] chunkTileValues = WorldInterface.Instance.GetChunkTileValues(mapMode.Property);

            if (chunkTileValues == null || chunkTileValues.Length == 0)
                return;

            int textureDimensions = _worldSize.ChunkCountPerAxis * _worldSize.TileAmountPerAxis;
            Texture2D bufferTexture = CreateBufferTexture(chunkTileValues, textureDimensions, mapMode.Colors);

            mapMode.WorldMapMaterial.SetTexture("_ValueTexture", bufferTexture);
            mapMode.WorldMapMaterial.SetVector(
                "_Tiling",
                new Vector4(textureDimensions, textureDimensions, 0, 0));
            mapMode.WorldMapMaterial.SetVector(
                "_Offset",
                new Vector4(1 / (textureDimensions * 2.0f), 1 / (textureDimensions * 2.0f), 0, 0));
            mapMode.WorldMapMaterial.SetFloat(
                "_DrawWaterAsBlue",
                mapMode.DrawWaterAsBlue ? 1 : 0);
            mapMode.WorldMapMaterial.SetColor(
                "_WaterColor",
                mapMode.WaterColor);
        }

        private static Texture2D CreateBufferTexture(
            TileValue[] chunkTileValues,
            int textureDimensions,
            Gradient colors)
        {
            Texture2D bufferTexture = new Texture2D(textureDimensions, textureDimensions, TextureFormat.RGBA32, false);
            bufferTexture.filterMode = FilterMode.Point;

            foreach (TileValue chunkTileValue in chunkTileValues)
            {
                Color color = colors.Evaluate(chunkTileValue.Value / 100);
                bufferTexture.SetPixel(
                    chunkTileValue.X,
                    chunkTileValue.Z,
                    color);
            }

            bufferTexture.Apply();
            return bufferTexture;
        }
    }
}