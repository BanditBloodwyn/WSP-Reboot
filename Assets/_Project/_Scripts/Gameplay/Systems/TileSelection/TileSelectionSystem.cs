using Assets._Project._Scripts.Gameplay.Systems.TileSelection.Settings;
using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.Generation;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Assets._Project._Scripts.Gameplay.Systems.TileSelection
{
    public class TileSelectionSystem : MonoBehaviour
    {
        [SerializeField] private TileSelectionSystemSettings _settings;
        [SerializeField] private UnityEvent<TileAspect> _tileSelected;
       
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
            UpdateTilePointingAt();

            if (Input.GetMouseButtonUp(0))
                SelectTile();
        }

        #endregion

        #region Pointing

        private void UpdateTilePointingAt()
        {
            Vector3 currentSelectionPosition = UpdateCurrentSelectionPosition(out ChunkComponent chunk);
            if (currentSelectionPosition == Vector3.zero)
                return;

            Vector3 currentSelectionTilePosition = chunk.GetNearestTilePositionFromPosition(currentSelectionPosition, out _currentPointedTile) + Vector3.up * 0.01f;
            UpdateSelectorPosition(currentSelectionTilePosition);
        }

        private static Vector3 UpdateCurrentSelectionPosition(out ChunkComponent chunk)
        {
            chunk = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                chunk = hit.transform.GetComponent<ChunkComponent>();
                return hit.point;
            }                
            return Vector3.zero;
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
            _tileSelected.Invoke(_currentPointedTile);
        }

        #endregion
    }
}