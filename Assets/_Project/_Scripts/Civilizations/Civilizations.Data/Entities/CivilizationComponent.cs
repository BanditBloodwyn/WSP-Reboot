using Assets._Project._Scripts.WorldMap.WorldMapCore.ECS.Aspects;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Assets._Project._Scripts.Civilizations.Civilizations.Data.Entities
{
    public struct CivilizationComponent : IComponentData
    {
        public FixedString64Bytes Name;
        public Color Color;
        public NativeArray<TileAspect> OwnedTiles;
    }
}