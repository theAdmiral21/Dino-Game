using UnityEngine;

namespace Application.Utility
{
    public static class PathUtils
    {
        public static Vector2 PickRandomXDest(float minDist, float maxDist, Vector2 currentPosition)
        {
            float x = Random.Range(minDist, maxDist);
            return currentPosition + new Vector2(x, 0);
        }
    }
}