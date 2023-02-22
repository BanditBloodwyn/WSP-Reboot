﻿using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.World.Components
{
    public struct Chunk
    {
        public long ID { get; set; }
        public Vector2Int Coordinates { get; set; }
        public int Size { get; set; }
        public Entity[] Tiles { get; set; }
        public Mesh Mesh { get; set; }
    }
}