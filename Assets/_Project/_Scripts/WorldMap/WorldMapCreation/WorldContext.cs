using System.Collections.Generic;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCreation
{
    public class WorldContext
    {
        public List<Chunk> Chunks { get; set; } = new();
        public List<ChunkComponent> ChunkComponents { get; set; } = new();
        public List<GameObject> SpawnedObjects { get; set; } = new();
    }
}