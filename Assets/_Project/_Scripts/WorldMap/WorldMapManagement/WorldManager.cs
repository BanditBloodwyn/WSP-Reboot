using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Settings.Scriptables;
using Assets._Project._Scripts.WorldMap.WorldMapCreation;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.ChunkObject;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Chunks;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Height;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Mesh;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.TileData;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Tiles;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using ChunkComponent = Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Components.ChunkComponent;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private WorldCreationParameters _worldCreationParameters;

        private EmptyTileAspect[] tiles;
        private ChunkComponent[] chunks;

        private void Awake()
        {
            Assert.IsNotNull(_worldCreationParameters);
            _worldCreationParameters.Init();
        }

        private IEnumerator Start()
        {
            //yield break;
            WorldGenerationPipeline pipeline = new();

            pipeline.AddStep(new ChunkGenerationStep());
            pipeline.AddStep(new TileGenerationStep());
            pipeline.AddStep(new HeightGenerationStep());
            pipeline.AddStep(new TileDataGenerationStep());
            pipeline.AddStep(new MeshGenerationStep());
            pipeline.AddStep(new ChunkObjectGenerationStep());

            WorldContext context = new();
            yield return pipeline.Execute(context, _worldCreationParameters);

            WorldInterface.Instance.Chunks.Clear();
            WorldInterface.Instance.Chunks.AddRange(context.Chunks);

            Events.OnWorldCreationFinished?.Invoke(this, null);
        }

        private void OnDrawGizmos()
        {
            DrawChunkGizmos();
            DrawTileGizmos();
        }

        private void DrawChunkGizmos()
        {
            if (chunks == null || chunks.Length == 0)
                chunks = WorldInterface.Instance.GetAllChunks();

            foreach (ChunkComponent chunk in chunks)
                Gizmos.DrawWireCube(
                    new Vector3(
                        chunk.TileAmountPerAxis * chunk.Coordinates.x - 0.5f,
                        0,
                        chunk.TileAmountPerAxis * chunk.Coordinates.y - 0.5f),
                    new Vector3(chunk.TileAmountPerAxis, 0, chunk.TileAmountPerAxis));
        }

        private void DrawTileGizmos()
        {
            if (tiles == null || tiles.Length == 0)
                tiles = WorldInterface.Instance.GetAllEmtpyTilesFromChunkId(0, 1, 64);

            foreach (EmptyTileAspect tile in tiles)
                Gizmos.DrawCube(
                    new Vector3(tile.Position.x, tile.Position.y, tile.Position.z),
                    new Vector3(1, 0, 1));
        }
    }
}