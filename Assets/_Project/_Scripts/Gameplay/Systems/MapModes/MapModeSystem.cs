using Assets._Project._Scripts.UI.MapModesUI.MapModes;
using Assets._Project._Scripts.WorldMap;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project._Scripts.Gameplay.Systems.MapModes
{
    public class MapModeSystem : MonoBehaviour
    {
        [SerializeField] private WorldCreationParameters _worldCreationParameters;
        [SerializeField] private Transform _chunkParent;

        [SerializeField] private UnityEvent _mapModeChosen;

        private void Awake()
        {
            Assert.IsNotNull(_chunkParent);
        }

        public void ChangeMapMode(MapMode mapMode)
        {
            if (mapMode.WorldMapMaterial == null)
            {
                Debug.LogError($"<color=#73BD73>Map mode system</color> - No material set for map mode \"<color=#73BD73>{mapMode.DisplayName}</color>\"");
                return;
            }

            if (mapMode.CalculateShaderBuffer)
                CalculateMaterialBuffer(mapMode);

            for (int i = 0; i < _chunkParent.childCount; i++)
                _chunkParent.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial = mapMode.WorldMapMaterial;

            _mapModeChosen?.Invoke();
        }

        private void CalculateMaterialBuffer(MapMode mapMode)
        {
            TileValue[] chunkTileValues = Landscape.Instance.GetChunkTileValues(mapMode.Property);

            if (chunkTileValues == null || chunkTileValues.Length == 0)
                return;

            int textureDimensions = _worldCreationParameters.WorldSize * _worldCreationParameters.ChunkSize;
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