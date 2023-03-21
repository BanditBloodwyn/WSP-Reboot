﻿using Assets._Project._Scripts.Gameplay.Helper;
using Assets._Project._Scripts.Gameplay.Systems.TileSelection.Settings;
using Assets._Project._Scripts.UI;
using Assets._Project._Scripts.UI.DataContainer;
using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.Generation;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets._Project._Scripts.Gameplay.Systems.TileSelection
{
    public class TileSelectionSystem : MonoBehaviour
    {
        [SerializeField] private TileSelectionSystemSettings _settings;

        private TileSelector _selector;
        private Vector3 _currentPointedPosition;
        private TileAspect _currentPointedTile;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_settings);
        }

        private void Start()
        {
            if (_selector == null)
            {
                _selector = Instantiate(_settings.SelectorPrefab, transform).GetComponent<TileSelector>();
                _selector.gameObject.name = "TileSelector";
            }
            _selector.SetSettings(_settings);
        }

        private void Update()
        {
            if (!UpdateTilePointingAt())
                return;

            if (Input.GetMouseButtonUp(0))
                SelectTile();

            _selector.gameObject.SetActive(!Input.GetMouseButton(2));
        }

        #endregion

        #region Pointing

        private bool UpdateTilePointingAt()
        {
            Vector3 currentSelectionPosition = PointerHelper.GetCurrentSelectionPosition(out ChunkComponent chunk);
            if (currentSelectionPosition == Vector3.zero)
                return false;

            if (!World.Landscape.Instance.TryGetTileFromChunkAndPosition(chunk, currentSelectionPosition, out _currentPointedTile))
                return false;

            UpdateSelectorPosition((Vector3)_currentPointedTile.Position + Vector3.up * 0.01f);
            return true;
        }

        private void UpdateSelectorPosition(Vector3 currentSelectionTilePosition)
        {
            if(_currentPointedPosition == currentSelectionTilePosition)
                return;

            _currentPointedPosition = currentSelectionTilePosition;
            _selector.StartMoveToPosition(_currentPointedPosition);
        }

        #endregion

        #region Selecting

        private void SelectTile()
        {
            UIManager.Instance.OpenMovablePopup(new TileSelectionDataContainer(_currentPointedTile));
        }

        #endregion
    }
}