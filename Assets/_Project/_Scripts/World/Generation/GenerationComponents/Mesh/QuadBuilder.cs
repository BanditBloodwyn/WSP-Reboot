using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation.GenerationComponents.Mesh
{
    public static class QuadBuilder
    {
        private static readonly Vector3[] VertPos =
        {
            new(-1, 0, -1), new(-1, 0, 1),
            new(1, 0, 1), new(1, 0, -1),
            new(-1, -5, -1), new(-1, -5, 1),
            new(1, -5, 1), new(1, -5, -1),
        };

        private static readonly int[,] Faces =
        {
            { 0, 1, 2, 3, 0, 1, 0, 0, 0 }, //top
            { 2, 1, 5, 6, 0, 0, 1, 1, 1 }, //right
            { 0, 3, 7, 4, 0, 0, -1, 1, 1 }, //left
            { 3, 2, 6, 7, 1, 0, 0, 1, 1 }, //front
            { 1, 0, 4, 5, -1, 0, 0, 1, 1 } //back
        };

        public static void AddQuad(
            float2 chunkOffset,
            List<Vector3> Verticies,
            Dictionary<float2, float3> voxelDictionary,
            int x, int z,
            int facenum,
            List<int> triangles,
            List<Vector2> uv)
        {
            int v = Verticies.Count;

            // Add Mesh
            for (int i = 0; i < 4; i++)
            {
                if (voxelDictionary.TryGetValue(new float2(x, z) + chunkOffset, out float3 tilePosition))
                    Verticies.Add(new Vector3(x, tilePosition.y, z) + VertPos[Faces[facenum, i]] / 2f);
            }

            triangles.AddRange(new List<int> { v, v + 1, v + 2, v, v + 2, v + 3 });

            // Add uvs
            Vector2 bottomleft = new Vector2(Faces[facenum, 7], Faces[facenum, 8]) / 2f;

            uv.AddRange(new List<Vector2>
            {
                bottomleft + new Vector2(0, 0.5f),
                bottomleft + new Vector2(0.5f, 0.5f),
                bottomleft + new Vector2(0.5f, 0),
                bottomleft
            });
        }
    }
}