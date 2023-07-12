using Assets._Project._Scripts.Core.EventSystem;
using Assets._Project._Scripts.WorldMap.WorldMapCore.Types;
using Assets._Project._Scripts.WorldMap.WorldMapCreation;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.ChunkObject;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Chunks;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Height;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Mesh;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.TileData;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.GenerationSteps.Tiles;
using Assets._Project._Scripts.WorldMap.WorldMapCreation.Settings.Scriptables;
using NUnit.Framework;
using System.Collections;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Assets._Project._Scripts.WorldMap.WorldMapManagement
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private WorldCreationParameters _worldCreationParameters;

        private void Awake()
        {
            Assert.IsNotNull(_worldCreationParameters);
            _worldCreationParameters.Init();
        }

        private IEnumerator Start()
        {
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

        private void Update()
        {
        }

        private void OnDrawGizmos()
        {
            foreach (Chunk chunk in WorldInterface.Instance.Chunks)
                Gizmos.DrawWireCube(new Vector3(chunk.Size * chunk.Coordinates.x, 0, chunk.Size * chunk.Coordinates.y), new Vector3(chunk.Size, 0, chunk.Size));

            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(LocalTransform));

            foreach (LocalTransform tile in entityQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp))
            {
                Gizmos.color = new Color(1, 1, 1, 0.1f);
                Gizmos.DrawWireCube(tile.Position, new Vector3(0.9f, 0.1f, 0.9f));
            }
        }
    }
}