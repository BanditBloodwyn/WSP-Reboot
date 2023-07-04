using Assets._Project._Scripts.Civilizations.Civilizations.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets._Project._Scripts.Civilizations.Features.Civilizations.Spawning
{
    public class CivilizationSpawner : MonoBehaviour
    {
        [Button]
        public bool Spawn()
        {
            CivilizationECSInterface.CreateCivilization("template", Color.red);
            return true;
        }
    }
}
