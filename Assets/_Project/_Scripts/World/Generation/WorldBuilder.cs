using Assets._Project._Scripts.World.Components;
using Assets._Project._Scripts.World.ECS.Components;
using Sirenix.OdinInspector;
using System.Collections;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Assets._Project._Scripts.World.Generation
{
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmos;

        [BoxGroup("Creation")]
        [SerializeField] private Transform _chunkParent;
        [BoxGroup("Creation")]
        [SerializeField] private Material _tileMaterial;
        
        [BoxGroup("Creation")]
        [Title("")]
        [SerializeField] private WorldCreationParameters _worldCreationParameters;

        [BoxGroup("Events")]
        [SerializeField] private UnityEvent _worldGenerationFinished;
        [BoxGroup("Events")]
        [SerializeField] private UnityEvent _worldDestructionStarted;

        #region Unity

        private void Awake()
        {
            Assert.IsNotNull(_chunkParent);
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

            foreach (Chunk chunk in World.Instance.Chunks)
            {
                Gizmos.DrawWireCube(new Vector3(chunk.Size * chunk.Coordinates.x, 0, chunk.Size * chunk.Coordinates.y), new Vector3(chunk.Size, 0, chunk.Size));
            }

            EntityQuery entityQuery = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(LocalTransform));

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
            ChunkBuilder.AdjustMaterialSettings(_tileMaterial, _worldCreationParameters.vegetationZoneHeights);

            for (int x = 0; x < _worldCreationParameters.WorldSize; x++)
            {
                for (int y = 0; y < _worldCreationParameters.WorldSize; y++)
                {
                    ChunkBuilder.Build(x, y, _worldCreationParameters, _chunkParent, _tileMaterial);
                    yield return null;
                }
            }

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

            EntityQuery entityQuery = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(ChunkAssignmentComponentData));
            foreach (Entity tile in entityQuery.ToEntityArray(Allocator.Temp))
                Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(tile);

            World.Instance.Chunks.Clear();
        }
    }
}