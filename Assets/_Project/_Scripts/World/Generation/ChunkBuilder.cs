using Assets._Project._Scripts.World.Components;
using Assets._Project._Scripts.World.ECS.Aspects;
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

        public static Entity[] BuildTiles(Chunk chunk, WorldCreationParameters parameters)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;

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
                        parameters);

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

            Dictionary<float2, float3> dictionary = positions.ToDictionary(
                static pos => new float2(pos.x, pos.z), 
                static pos => pos);

            for (int x = -chunkSize / 2; x < chunkSize / 2; x++)
            {
                for (int z = -chunkSize / 2; z < chunkSize / 2; z++)
                {
                    Vector3[] VertPos =
                    {
                        new(-1, 1, -1), new(-1, 1, 1),
                        new(1, 1, 1), new(1, 1, -1),
                        new(-1, -5, -1), new(-1, -5, 1),
                        new(1, -5, 1), new(1, -5, -1),
                    };

                    int[,] Faces =
                    {
                        { 0, 1, 2, 3, 0, 1, 0, 0, 0 }, //top
                        { 7, 6, 5, 4, 0, -1, 0, 1, 0 }, //bottom
                        { 2, 1, 5, 6, 0, 0, 1, 1, 1 }, //right
                        { 0, 3, 7, 4, 0, 0, -1, 1, 1 }, //left
                        { 3, 2, 6, 7, 1, 0, 0, 1, 1 }, //front
                        { 1, 0, 4, 5, -1, 0, 0, 1, 1 } //back
                    };

                    for (int o = 0; o < 6; o++)
                        AddQuad(o, Verticies.Count);

                    void AddQuad(int facenum, int v)
                    {
                        // Add Mesh
                        for (int i = 0; i < 4; i++)
                        {
                            if(dictionary.TryGetValue(new float2(x, z) + chunkOffset, out float3 tilePosition)) 
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

            Mesh mesh = new Mesh
            {
                vertices = Verticies.ToArray(),
                triangles = triangles.ToArray(),
                uv = uv.ToArray()
            };
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}