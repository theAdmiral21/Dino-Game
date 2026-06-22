using UnityEngine;

namespace Tools.DesignTools
{
    public class PathCast
    {
        private LayerMask _layerMask;
        public PathCast(LayerMask layerMask)
        {
            _layerMask = layerMask;
        }
        /// <summary>
        /// Method for performing a ray cast beginning at s0 and points towards s1. Returns the location for a point of contact.
        /// </summary>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        public RaycastHit2D Cast(Vector2 s0, Vector2 s1)
        {
            Vector2 dir = (s1 - s0).normalized;
            float dist = (s1 - s0).magnitude;
            return Physics2D.Raycast(s0, dir, dist, _layerMask);
        }
    }
}