﻿using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapCore.Types
{
    public class ChunkComponent : MonoBehaviour
    {
        public long ID;
        public Entity[] Tiles;
    }
}