using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.World.Components
{
    public class ChunkComponent : MonoBehaviour
    {
        public long ID;
        public Entity[] Tiles;
    }
}