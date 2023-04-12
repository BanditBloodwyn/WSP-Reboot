using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.WorldMap.ECS.Aspects;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets._Project._Scripts.WorldMap.Generation.GenerationComponents.Mesh
{
    public class MeshBuilderComponent : IGenerationComponent
    {
        [SerializeField] private bool _enabled;

        public bool Enabled => _enabled;

        public void Apply(Chunk chunk)
        {
            List<float3> tilePositions = new();

            foreach (Entity tileEntity in chunk.Tiles)
            {
                EmptyTileAspect tileAspect = tileEntity.GetEmptyTileAspect();
                tilePositions.Add(tileAspect.Position);
            }

            chunk.Mesh = GenerateMeshFromVoxelPositions(tilePositions.ToArray(), chunk.Size, new float2(chunk.Size * chunk.Coordinates.x, chunk.Size * chunk.Coordinates.y));
        }

        private static UnityEngine.Mesh GenerateMeshFromVoxelPositions(float3[] positions, int chunkSize, float2 chunkOffset)
        {
            List<int> triangles = new();
            List<Vector3> Verticies = new();
            List<Vector2> uv = new();

            Dictionary<float2, float3> voxelDictionary = positions.ToDictionary(
                static pos => new float2(pos.x, pos.z),
                static pos => pos);

            for (int x = -chunkSize / 2; x < chunkSize / 2; x++)
            for (int z = -chunkSize / 2; z < chunkSize / 2; z++)
            for (int facenum = 0; facenum < 5; facenum++)
                QuadBuilder.AddQuad(chunkOffset, Verticies, voxelDictionary, x, z, facenum, triangles, uv);


            UnityEngine.Mesh mesh = new();
            mesh.indexFormat = IndexFormat.UInt32;

            mesh.vertices = Verticies.ToArray();
            mesh.triangles = triangles.ToArray();
            mesh.uv = uv.ToArray();

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            return mesh;
        }
    }
}