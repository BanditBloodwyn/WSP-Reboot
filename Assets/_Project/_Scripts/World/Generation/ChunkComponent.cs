using Assets._Project._Scripts.World.ECS.Aspects;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Assets._Project._Scripts.World.Generation
{
    public class ChunkComponent : MonoBehaviour
    {
        public long ID;
        public Entity[] Tiles;

        public Vector3 GetNearestTilePositionFromPosition(Vector3 roundedPosition, out TileAspect nearestTile)
        {
            EntityManager entityManager = Unity.Entities.World.DefaultGameObjectInjectionWorld.EntityManager;
           
            float nearestDistance = float.MaxValue;
            float3 nearestPosition = float3.zero;
            nearestTile = new TileAspect();

            foreach (Entity entity in Tiles)
            {
                if (entityManager.Exists(entity))
                {
                    float3 position = entityManager.GetComponentData<LocalTransform>(entity).Position;
                    float distance = math.distance(roundedPosition, position);
                    
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestPosition = position;
                        nearestTile = entityManager.GetAspect<TileAspect>(entity);
                    }
                }
            }

            return new Vector3(nearestPosition.x, nearestPosition.y, nearestPosition.z);
        }
    }
}