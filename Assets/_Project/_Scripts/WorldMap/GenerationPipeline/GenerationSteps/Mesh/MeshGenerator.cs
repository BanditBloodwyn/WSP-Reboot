using System.Collections.Generic;
using System.Linq;
using Assets._Project._Scripts.Features.WorldMap.WorldMapCore.Types;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Mesh
{
    public class MeshGenerator
    {
        public static UnityEngine.Mesh GenerateMeshFromVoxelPositions(Chunk chunk, float3[] positions)
        {
            List<int> triangles = new();
            List<Vector3> Verticies = new();
            List<Vector2> uv = new();

            Dictionary<float2, float3> voxelDictionary = positions.ToDictionary(
                static pos => new float2(pos.x, pos.z),
                static pos => pos);

            float2 chunkOffset = new float2(chunk.Size * chunk.Coordinates.x, chunk.Size * chunk.Coordinates.y);

            for (int x = -chunk.Size / 2; x < chunk.Size / 2; x++)
                for (int z = -chunk.Size / 2; z < chunk.Size / 2; z++)
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