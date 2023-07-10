using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.CoreFeatures.SelectionSystem.Settings;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Helpers;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapManagement;
using UnityEngine;
using UnityEngine.Assertions;
using TileAspect = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects.TileAspect;

namespace Assets._Project._Scripts.CoreFeatures.SelectionSystem
{
    public class TileSelectionSystem : MonoBehaviour
    {
        [SerializeField] private TileSelectionSystemSettings _settings;

        private TileSelector _selector;
        private Vector3 _currentPointedPosition;

        public TileAspect CurrentPointedTile { get; private set; }

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
            if (_settings.RealTimeSelectorMovement)
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

            if (!WorldInterface.Instance.TryGetTileFromChunkAndPosition(chunk, currentSelectionPosition, out TileAspect? currentPointedTile))
                return false;

            CurrentPointedTile = currentPointedTile!.Value;
            UpdateSelectorPosition((Vector3)CurrentPointedTile.Position + Vector3.up * 0.01f);
            return true;
        }

        private void UpdateSelectorPosition(Vector3 currentSelectionTilePosition)
        {
            if (_currentPointedPosition == currentSelectionTilePosition)
                return;

            _currentPointedPosition = currentSelectionTilePosition;
            _selector.StartMoveToPosition(_currentPointedPosition);
        }

        #endregion


        #region Selecting

        private void SelectTile()
        {
            if (!_settings.RealTimeSelectorMovement)
                if (!UpdateTilePointingAt())
                    return;

            Events.OnTileSelected.Invoke(this, CurrentPointedTile);
        }

        #endregion
    }
}