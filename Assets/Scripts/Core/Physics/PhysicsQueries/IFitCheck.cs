using Physics.Core.DataStructures;
using UnityEngine;

namespace Core.Physics.PhysicsQueries
{
    public interface IFitCheck
    {
        /// <summary>
        /// Method for determining if the player will fit into a given area centered on center.
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        public bool CheckFit(Vector2 center, ref RaycastConfiguration rayConfig);

        /// <summary>
        /// Method for determining if the player will fit into a given area centered on center.
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        public bool CheckFit(Vector2 center, Vector2 size, int layerMask);
    }
}