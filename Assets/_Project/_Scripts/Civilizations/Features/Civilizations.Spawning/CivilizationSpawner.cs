using Assets._Project._Scripts.Civilizations.Civilizations.Data;
using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using UnityEngine;

namespace Assets._Project._Scripts.Civilizations.Features.Civilizations.Spawning
{
    public class CivilizationSpawner : MonoBehaviour
    {
        #region Unity

        private void OnEnable()
        {
            Events.OnCreateCivilization.AddListener(Spawn);
        }

        private void OnDisable()
        {
            Events.OnCreateCivilization.RemoveListener(Spawn);
        }

        #endregion

        private object Spawn(Component sender, object data)
        {
            if (data is not TileAspect tile)
                return null;

            SpawnInternal("testCiv", Color.red, tile.Position);
            
            return null;
        }

        public bool SpawnInternal(string civilizationName, Color color, Vector3 center)
        {
            CivilizationECSInterface.CreateCivilization(civilizationName, color, center);
            return true;
        }
    }
}
