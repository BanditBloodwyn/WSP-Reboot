using UnityEngine;

namespace Assets._Project._Scripts.Core.Extentions
{
    public static class Vector3Extentions
    {
        public static Vector3 Get2D(this Vector3 vector)
        {
            vector.y = 0;
            return vector.normalized;
        }
    }
}