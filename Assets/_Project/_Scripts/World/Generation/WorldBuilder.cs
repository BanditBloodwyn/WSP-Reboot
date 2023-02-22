using Assets._Project._Scripts.World.ECS.Components;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Assertions;
using ChunkComponent = Assets._Project._Scripts.World.Components.ChunkComponent;

namespace Assets._Project._Scripts.World.Generation
{
    [LabelWidth(150)]
    public class WorldBuilder : MonoBehaviour
    {
        [SerializeField] private bool _drawGizmos;

        [Title("Creation", TitleAlignment = TitleAlignments.Centered)]

        [SerializeField] private Transform _chunkParent;
        [SerializeField] private WorldCreationParameters _worldCreationParameters;
        [SerializeField] private Material _tileMaterial;

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

            foreach (ChunkComponent chunk in World.Instance.Chunks)
            {
                Gizmos.DrawWireCube(chunk.transform.localPosition, new Vector3(chunk.Size, 0, chunk.Size));
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
            LandscapeBuilder.Build(_worldCreationParameters, _chunkParent, _tileMaterial);
            
            Debug.Log($"Finished world building - Chunk count = {World.Instance.Chunks.Count}");
        }

        [Button(Icon = SdfIconType.Globe2, IconAlignment = IconAlignment.LeftOfText, ButtonHeight = 31)]
        [ButtonGroup("World")]
        [GUIColor(0.7f, 0, 0)]
        public void DestroyWorld()
        {
            for (int i = _chunkParent.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = _chunkParent.transform.GetChild(i);
                DestroyImmediate(child.gameObject);
            }

            EntityQuery entityQuery = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(ChunkAssignmentComponent));
            foreach (Entity tile in entityQuery.ToEntityArray(Allocator.Temp))
            {
                Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(tile);
            }

            World.Instance.Chunks.Clear();
        }
    }
}
