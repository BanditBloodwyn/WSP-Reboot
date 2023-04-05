using System;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.ECS.Components
{
    public class ChunkMaterialComponent : IComponentData, IDisposable
    {
        public MeshRenderer MeshRenderer;
        public void Dispose()
        {
        }
    }
}