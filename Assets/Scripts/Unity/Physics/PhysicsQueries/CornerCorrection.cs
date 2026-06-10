using System.Collections.Generic;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsQueries;
using UnityEngine;

namespace Physics.Unity.PhysicsQueries
{
    public class CornerCorrection : ICornerResolver
    {
        private float _horizontalCornerLimit = 0.3f;
        private float _verticalCornerLimit = 0.3f;
        protected LayerMask _collisionMask;
        public CornerCorrection()
        {
            _collisionMask = LayerMask.GetMask("Collision");
        }
        public Vector2 CalculateHorizontalNudge(RaycastResult cornerRay, RaycastConfiguration rayConfig)
        {
            // Determine where the edge of this corner is
            bool isColliding = IsColliding(cornerRay.Origin, cornerRay.Direction, cornerRay.Distance);
            int i = 0;
            float step = 0.01f;
            Vector2 nudgeDir = UpDown(cornerRay, rayConfig);
            Vector2 correctionVector = step * i * nudgeDir;
            while (isColliding && correctionVector.y < _horizontalCornerLimit)
            {
                i++;
                correctionVector = step * i * nudgeDir;

                isColliding = IsColliding(cornerRay.Origin + correctionVector, cornerRay.Direction, cornerRay.Distance);
            }
            // By this point we should have a correction vector.
            if (correctionVector.y >= _horizontalCornerLimit) return Vector2.zero;

            return correctionVector;
        }



        public Vector2 CalculateVerticalNudge(RaycastResult cornerRay, RaycastConfiguration rayConfig)
        {
            // Determine where the edge of this corner is
            bool isColliding = IsColliding(cornerRay.Origin, cornerRay.Direction, cornerRay.Distance);
            int i = 0;
            float step = 0.01f;
            Vector2 nudgeDir = LeftRight(cornerRay, rayConfig);
            // Debug.Log($"Corner ray nudge dir: {nudgeDir}");
            Vector2 correctionVector = step * i * nudgeDir;
            while (isColliding && correctionVector.x < _verticalCornerLimit)
            {
                i++;
                correctionVector = step * i * nudgeDir;

                isColliding = IsColliding(cornerRay.Origin + correctionVector, cornerRay.Direction, cornerRay.Distance);
            }
            // By this point we should have a correction vector.
            if (correctionVector.x >= _verticalCornerLimit) return Vector2.zero;

            return correctionVector;
        }

        /// <summary>
        /// Method for checking which raycast result found a corner, -1 means no corner was found.
        /// </summary>
        /// <returns>int</returns>
        public int CornerCheck(List<RaycastResult> raycasts)
        {
            // If the first and last raycast, the edges, made contact there isn't a corner
            if (raycasts[0].GotHit && raycasts[^1].GotHit)
            {
                return -1;
            }

            // We need more info, get the contact count
            int contactCount = 0;
            for (int i = 0; i < raycasts.Count; i++)
            {
                // If something hit, we might have a corner
                if (raycasts[i].GotHit)
                {
                    contactCount += 1;
                }
            }

            // if we have no contacts, we have no corner
            if (contactCount == 0) return -1;

            // If there is only one contact, and that contact is an edge, then we have a corner.
            if (contactCount == 1 && (raycasts[0].GotHit || raycasts[^1].GotHit))
            {
                // return which result found the corner
                if (raycasts[0].GotHit) return 0;
                // Make sure to index correctly
                return raycasts.Count - 1;
            }
            return -1;
        }

        private bool IsColliding(Vector2 origin, Vector2 direction, float dist)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, dist, _collisionMask);
            // I have to debug this
            Debug.DrawRay(origin, direction * dist, Color.red, 0.1f);
            return hit.collider != null;
        }
        private Vector2 LeftRight(RaycastResult cornerRay, RaycastConfiguration rayConfig)
        {
            // How do we know if we need to scan up/down? or left/right?

            // Find where the corner ray's origin lines up with the raycast configuration
            Vector2 cornerOrigin = cornerRay.Origin;
            float leftDiff = Mathf.Abs(cornerOrigin.x - rayConfig.Bounds.Left);
            float rightDiff = Mathf.Abs(cornerOrigin.x - rayConfig.Bounds.Right);
            // Debug.Log($"corner origin - left: {}");
            // Debug.Log($"corner origin - right: {cornerOrigin.x - rayConfig.Bounds.Right}");

            if (leftDiff < rightDiff) return Vector2.right;

            if (leftDiff > rightDiff) return Vector2.left;

            return Vector2.right;
        }

        private Vector2 UpDown(RaycastResult cornerRay, RaycastConfiguration rayConfig)
        {
            // How do we know if we need to scan up/down? or left/right?

            // Find where the corner ray's origin lines up with the raycast configuration
            Vector2 cornerOrigin = cornerRay.Origin;
            float bottomDiff = Mathf.Abs(cornerOrigin.y - rayConfig.Bounds.Bottom);
            float topDiff = Mathf.Abs(cornerOrigin.y - rayConfig.Bounds.Top);

            if (bottomDiff < topDiff) return Vector2.up;

            if (bottomDiff > topDiff) return Vector2.down;

            return Vector2.up;
        }

    }
}