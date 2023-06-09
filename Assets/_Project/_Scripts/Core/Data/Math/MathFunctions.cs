using System;

namespace Assets._Project._Scripts.Core.Data.Math
{
    public static class MathFunctions
    {
        public static float Bump(float value, float bumpHeight, float bumpWidth, float offset, float steepness)
        {
            return bumpHeight / (1 + MathF.Pow(MathF.Abs((value - offset) / bumpWidth), steepness));
        }
    }
}