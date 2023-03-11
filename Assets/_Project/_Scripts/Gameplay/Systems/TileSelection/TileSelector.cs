using Assets._Project._Scripts.Gameplay.Systems.TileSelection.Settings;
using System.Collections;
using UnityEngine;

namespace Assets._Project._Scripts.Gameplay.Systems.TileSelection
{
    public class TileSelector : MonoBehaviour
    {
        private TileSelectionSystemSettings _settings;
        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private float elapsedTime;

        public void SetSettings(TileSelectionSystemSettings settings)
        {
            _settings = settings;

            GetComponent<MeshRenderer>().sharedMaterial.color = _settings.SelectorTint;
        }

        public void StartMoveToPosition(Vector3 newPosition)
        {
            _startPosition = transform.position;
            _endPosition = newPosition;
        
            StopCoroutine("MoveToPosition");
            StartCoroutine("MoveToPosition");
        }

        private IEnumerator MoveToPosition()
        {
            elapsedTime = 0;

            while (elapsedTime <= _settings.HighlightFadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float percentageCompleted = elapsedTime / _settings.HighlightFadeDuration;
               
                transform.position = Vector3.Lerp(
                    _startPosition,
                    _endPosition,
                    1 - (1 - percentageCompleted) * (1 - percentageCompleted));
              
                yield return null;
            }

            transform.position = _endPosition;
        }
    }
}