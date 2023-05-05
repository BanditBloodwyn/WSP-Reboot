using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline
{
    public class WorldContext
    {
        public List<Chunk> Chunks { get; set; } = new();
        public List<GameObject> SpawnedObjects { get; set; } = new();
    }
}