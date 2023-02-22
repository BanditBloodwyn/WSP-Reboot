using System.Collections.Generic;
using Assets._Project._Scripts.World.Components;

namespace Assets._Project._Scripts.World
{
    public class World
    {
        #region Singleton

        private static World instance;

        private World() { }

        public static World Instance => instance ??= new World();

        #endregion

        public List<Chunk> Chunks = new();
    }
}