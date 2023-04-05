using Assets._Project._Scripts.WorldMap.Data.Enums;
using Assets._Project._Scripts.WorldMap.Data.Structs;
using Assets._Project._Scripts.WorldMap.ECS.Systems;
using Unity.Entities;

namespace Assets._Project._Scripts.WorldMap.ECS.Helpers
{
    public class GetChunkValuesHelper
    {
        private Entity _bufferEntity;

        public void Init()
        {
            _bufferEntity = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntity();
            World.DefaultGameObjectInjectionWorld.EntityManager.AddBuffer<GetTileValuesBufferElement>(_bufferEntity);
        }

        public TileValue[] GetChunkTileValues(TileProperties property)
        {
            World.DefaultGameObjectInjectionWorld.EntityManager
                .GetBuffer<GetTileValuesBufferElement>(_bufferEntity)
                .Add(new GetTileValuesBufferElement { Property = property });

            return null;
        }
    }
}