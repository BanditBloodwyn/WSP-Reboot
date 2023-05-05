using Assets._Project._Scripts.WorldMap.Generation;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Assets._Project._Scripts.WorldMap.GenerationPipeline;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.ChunkObject;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Chunks;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Height;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Mesh;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.TileData;
using Assets._Project._Scripts.WorldMap.GenerationPipeline.GenerationSteps.Tiles;
using NUnit.Framework;
using System.Collections;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Events;

namespace Assets._Project._Scripts.WorldMap
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private WorldCreationParameters _worldCreationParameters;
        [SerializeField] private UnityEvent _worldGenerationFinished;

        private void Awake()
        {
            Assert.IsNotNull(_worldCreationParameters);
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

            _worldGenerationFinished?.Invoke();
        }

        private void Update()
        {
        }

        private void OnDrawGizmos()
        {
            foreach (Chunk chunk in Landscape.Instance.Chunks)
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