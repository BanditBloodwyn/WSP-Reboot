using Assets._Project._Scripts.World.Components;
using Assets._Project._Scripts.World.ECS.Aspects;
using Assets._Project._Scripts.World.Generation.Helper;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public static class ChunkBuilder
    {
        public static Chunk Build(int xCoord, int yCoord, WorldCreationParameters parameters, Material tileMaterial)
        {
            Chunk chunk = new Chunk();
            chunk.ID = yCoord * parameters.ChunkSize + xCoord;
            chunk.Coordinates = new(xCoord, yCoord);
            chunk.Size = parameters.ChunkSize;

            World.Instance.Chunks.Add(chunk);

            return chunk;
        }

        public static Entity[] BuildTiles(Chunk chunk, WorldCreationParameters parameters, out float minHeight, out float maxHeight)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

            maxHeight = float.NegativeInfinity;
            minHeight = float.PositiveInfinity;

            List<Entity> entities = new();

            for (int x = 0; x < chunk.Size; x++)
            {
                for (int y = 0; y < chunk.Size; y++)
                {
                    Entity tile = TileBuilder.Build(
                        entityManager,
                        x + chunk.Size * chunk.Coordinates.x - chunk.Size / 2,
                        y + chunk.Size * chunk.Coordinates.y - chunk.Size / 2,
                        chunk.Coordinates.y * chunk.Size + chunk.Coordinates.x,
                        parameters,
                        out float height);

                    if (height > maxHeight)
                        maxHeight = height;
                    if (height < minHeight)
                        minHeight = height;

                    entities.Add(tile);
                }
            }

            return entities.ToArray();
        }

        public static Mesh CreateChunkMesh(Chunk chunk)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

            List<float3> tilePositions = new();

            foreach (Entity tileEntity in chunk.Tiles)
            {
                TileAspect tileAspect = entityManager.GetAspect<TileAspect>(tileEntity);
                tilePositions.Add(tileAspect.Position);
            }

            return GenerateMeshFromVoxelPositions(tilePositions.ToArray(), chunk.Size, new float2(chunk.Size * chunk.Coordinates.x, chunk.Size * chunk.Coordinates.y));
        }

        public static Mesh GenerateMeshFromVoxelPositions(float3[] positions, int chunkSize, float2 chunkOffset)
        {
            List<int> triangles = new List<int>();
            List<Vector3> Verticies = new List<Vector3>();
            List<Vector2> uv = new List<Vector2>();

            Dictionary<float2, float3> voxelDictionary = positions.ToDictionary(
                static pos => new float2(pos.x, pos.z),
                static pos => pos);

            for (int x = -chunkSize / 2; x < chunkSize / 2; x++)
                for (int z = -chunkSize / 2; z < chunkSize / 2; z++)
                    for (int facenum = 0; facenum < 5; facenum++)
                        QuadBuilder.AddQuad(chunkOffset, Verticies, voxelDictionary, x, z, facenum, triangles, uv);

            Mesh mesh = new Mesh
            {
                vertices = Verticies.ToArray(),
                triangles = triangles.ToArray(),
                uv = uv.ToArray()
            };
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();

            return mesh;
        }
    }
}