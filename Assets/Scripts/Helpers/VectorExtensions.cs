using UnityEngine;

namespace Helpers
{
    public static class VectorExtensions
    {
        public static Vector3 ToVector3(this Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }
    }
}