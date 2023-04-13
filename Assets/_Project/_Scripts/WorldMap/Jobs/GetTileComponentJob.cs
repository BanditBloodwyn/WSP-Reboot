using Assets._Project._Scripts.WorldMap.ECS.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

namespace Assets._Project._Scripts.WorldMap.Jobs
{
    public partial struct GetTileComponentJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Entity> TileEntities;
        [ReadOnly] public NativeArray<TilePropertiesComponentData> TileComponenets;

        [ReadOnly] public ComponentLookup<TilePropertiesComponentData> TileData;

        public void Execute(int index)
        {
            Entity entity = TileEntities[index];

            if (!TileData.HasComponent(entity))
                return;

            TileComponenets[index] = TileData[entity];
        }
    }
}