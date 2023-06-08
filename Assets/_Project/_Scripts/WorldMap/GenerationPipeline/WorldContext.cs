using System.Collections.Generic;
using Assets._Project._Scripts.Features.WorldMap.WorldMapCore.Types;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline
{
    public class WorldContext
    {
        public List<Chunk> Chunks { get; set; } = new();
        public List<GameObject> SpawnedObjects { get; set; } = new();
    }
}