using Assets._Project._Scripts.World.Generation;
using NUnit.Framework;
using UnityEngine;

namespace Assets._Project._Scripts.World
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private WorldBuilder _worldBuilder;

        private void Awake()
        {
            Assert.IsNotNull(_worldBuilder);
        }

        private void Start()
        {
            _worldBuilder.BuildNewWorld();
        }

        private void Update()
        {
        }
    }
}