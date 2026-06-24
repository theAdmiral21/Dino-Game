using System.Collections.Generic;
using Core.Physics.Collisions.DataStructures;
using Physics.Core.DataStructures;
using UnityEngine;

namespace Physics.Application.Abstractions
{
    public interface IRaycastController
    {

        /// <summary>
        /// Method for casting rays in the left or right direction to determine collision
        /// </summary>
        /// <param name="velocity"></param>
        public Vector2 HorizontalRaycast(ref Vector2 velocity, ref RaycastConfiguration rayConfig);
        /// <summary>
        /// Method for casting rays in the up or down direction to determine collision
        /// </summary>
        /// <param name="velocity"></param>
        public Vector2 VerticalRaycast(ref Vector2 velocity, ref RaycastConfiguration rayConfig);
        /// <summary>
        /// Method for casting a ray from a corner to determine collision
        /// </summary>
        /// <param name="velocity"></param>
        public void CornerRayCast(ref Vector2 velocity, ref RaycastConfiguration rayConfig);

        /// <summary>
        /// Method for resting the collision info
        /// </summary>
        public void ResetCollisions();
        /// <summary>
        /// Method for checking if the player is close enough to a wall to perform a wall jump.
        /// </summary>
        /// <returns></returns>
        // public bool WallJumpCheck();

        /// <summary>
        /// Method for determining if the player will fit into a given area centered on center.
        /// </summary>
        /// <param name="center"></param>
        /// <returns></returns>
        public bool CheckFit(Vector2 center, ref RaycastConfiguration rayConfig);

        /// <summary>
        /// Method that returns a hash set of all of the collisions gathered this frame. This is meant to be called after Horizontal and Vertical raycast methods have been called.
        /// </summary>
        /// <returns></returns>
        List<RayCollision> GetCollisions();
    }
}