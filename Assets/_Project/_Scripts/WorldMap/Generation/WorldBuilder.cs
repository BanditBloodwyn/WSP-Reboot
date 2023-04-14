using System.Collections;
using System.Linq;
using Assets._Project._Scripts.WorldMap.ECS.Components;
using Assets._Project._Scripts.WorldMap.Generation.GenerationComponents;
using Assets._Project._Scripts.WorldMap.Generation.Settings;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Assets._Project._Scripts.WorldMap.Generation
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmos;

        [FoldoutGroup("Creation")]
        [SerializeField] private Transform _chunkParent;

        [FoldoutGroup("Creation")]
        [SerializeField] private WorldCreationParameters _worldCreationParameters;

        [FoldoutGroup("Events")]
        [SerializeField] private UnityEvent _worldGenerationFinished;
        [FoldoutGroup("Events")]
        [SerializeField] private UnityEvent _worldDestructionStarted;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_chunkParent);
            Assert.IsNotNull(_worldCreationParameters);
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;

            foreach (Chunk chunk in Landscape.Instance.Chunks)
            {
                Gizmos.DrawWireCube(new Vector3(chunk.Size * chunk.Coordinates.x, 0, chunk.Size * chunk.Coordinates.y), new Vector3(chunk.Size, 0, chunk.Size));
            }

            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(LocalTransform));

            foreach (LocalTransform tile in entityQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp))
            {
                Gizmos.color = new Color(1, 1, 1, 0.1f);
                Gizmos.DrawWireCube(tile.Position, new Vector3(0.9f, 0.1f, 0.9f));
            }
        }

        #endregion

        [PropertySpace]
        [Button(Icon = SdfIconType.Globe2, IconAlignment = IconAlignment.LeftOfText, ButtonHeight = 31)]
        [ButtonGroup("World")]
        [GUIColor(0, 0.7f, 0)]
        public void BuildNewWorld()
        {
            DestroyWorld();

            StartCoroutine(BuildWorld());
        }

        private IEnumerator BuildWorld()
        {
            for (int x = 0; x < _worldCreationParameters.WorldSize; x++)
            {
                for (int y = 0; y < _worldCreationParameters.WorldSize; y++)
                {
                    Chunk chunk = new Chunk();

                    chunk.ID = y * _worldCreationParameters.ChunkSize + x;
                    chunk.Coordinates = new Vector2Int(x, y);
                    chunk.Size = _worldCreationParameters.ChunkSize;

                    foreach (IGenerationComponent component in _worldCreationParameters.GenerationComponents)
                        if(component != null && component.Enabled) 
                            component.Apply(chunk);

                    Landscape.Instance.Chunks.Add(chunk);

                    
                    yield return null;
                }
            }

            Debug.Log($"Finished creating world. Total tile count {Landscape.Instance.Chunks.Sum(static chunk => chunk.Tiles.Length)}");

            _worldGenerationFinished?.Invoke();
        }

        [Button(Icon = SdfIconType.Globe2, IconAlignment = IconAlignment.LeftOfText, ButtonHeight = 31)]
        [ButtonGroup("World")]
        [GUIColor(0.7f, 0, 0)]
        public void DestroyWorld()
        {
            _worldDestructionStarted?.Invoke();
            StopAllCoroutines();

            for (int i = _chunkParent.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = _chunkParent.transform.GetChild(i);
                DestroyImmediate(child.gameObject);
            }

            EntityQuery entityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(ChunkAssignmentComponentData));
            foreach (Entity tile in entityQuery.ToEntityArray(Allocator.Temp))
                World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(tile);

            Landscape.Instance.Chunks.Clear();
        }
    }
}