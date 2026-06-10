using System.Collections.Generic;
using System.ComponentModel;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsQueries;
using UnityEngine;

namespace Physics.Unity.Physics
{
    /// <summary>
    /// Simple helper class for performing raycasts
    /// </summary>
    public class RaycastProbe
    {
        // public int RaycastCount { get; set; }
        // public CollisionInfo CollisionData;
        private LayerMask _collisionMask;

        public RaycastProbe(LayerMask collisionMask)
        {
            _collisionMask = collisionMask;
        }

        public void SetCollisionMask(LayerMask mask)
        {
            _collisionMask = mask;
        }

        /// <summary>
        /// Method for casting rays downward along the length of the Raycaster's provided collider. This cast returns the first hits that it gets.
        /// </summary>
        /// <param name="dist"></param>
        /// <returns></returns>
        public Collider2D CastDown(float dist, RaycastConfiguration rayConfig, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            for (int i = 0; i < rayConfig.RaycastCountVertical; i++)
            {
                Vector2 origin = rayConfig.Origins.BottomLeft + (rayConfig.RaySpacingX * i);
                Vector2 dir = Vector2.down;
                RaycastHit2D hits = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(origin, dir * dist, debugColor);
                }
                if (hits)
                {
                    return hits.collider;
                }
            }
            return null;
        }

        public Collider2D CastUp(float dist, RaycastConfiguration rayConfig, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            for (int i = 0; i < rayConfig.RaycastCountVertical; i++)
            {
                Vector2 origin = rayConfig.Origins.TopLeft + (rayConfig.RaySpacingX * i);
                Vector2 dir = Vector2.up;
                RaycastHit2D hits = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(origin, dir * dist, debugColor);
                }
                if (hits)
                {
                    return hits.collider;
                }
            }
            return null;
        }

        public Collider2D CastLeft(float dist, RaycastConfiguration rayConfig, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            for (int i = 0; i < rayConfig.RaycastCountHorizontal; i++)
            {
                Vector2 origin = rayConfig.Origins.BottomLeft + (rayConfig.RaySpacingY * i);
                Vector2 dir = Vector2.left;
                RaycastHit2D hits = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(origin, dir * dist, debugColor);
                }
                if (hits)
                {
                    return hits.collider;
                }
            }
            return null;
        }

        public Collider2D CastRight(float dist, RaycastConfiguration rayConfig, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            for (int i = 0; i < rayConfig.RaycastCountHorizontal; i++)
            {
                Vector2 origin = rayConfig.Origins.BottomRight + (rayConfig.RaySpacingY * i);
                Vector2 dir = Vector2.right;
                RaycastHit2D hits = Physics2D.Raycast(origin, dir, dist, _collisionMask);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(origin, dir * dist, debugColor);
                }
                if (hits)
                {
                    return hits.collider;
                }
            }
            return null;
        }

        public List<Collider2D> CastAllDown(float dist, RaycastConfiguration rayConfig)
        {
            rayConfig.UpdateRaycastOrigins();
            List<Collider2D> results = new();
            for (int i = 0; i < rayConfig.RaycastCountVertical; i++)
            {
                Vector2 origin = rayConfig.Origins.BottomLeft + (rayConfig.RaySpacingX * i);
                Vector2 dir = Vector2.down;
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, dist, _collisionMask);
                Debug.DrawRay(origin, dir * dist, Color.black);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        results.Add(hit.collider);
                    }
                }
            }
            return results;
        }

        public List<Collider2D> CastAllUp(float dist, RaycastConfiguration rayConfig)
        {
            rayConfig.UpdateRaycastOrigins();
            List<Collider2D> results = new();
            for (int i = 0; i < rayConfig.RaycastCountVertical; i++)
            {
                Vector2 origin = rayConfig.Origins.TopLeft + (rayConfig.RaySpacingX * i);
                Vector2 dir = Vector2.up;
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hits: {hits.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.black);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        results.Add(hit.collider);
                    }
                }
            }
            return results;
        }

        public List<Collider2D> CastAllLeft(float dist, RaycastConfiguration rayConfig)
        {
            rayConfig.UpdateRaycastOrigins();
            List<Collider2D> results = new();
            for (int i = 0; i < rayConfig.RaycastCountHorizontal; i++)
            {
                Vector2 origin = rayConfig.Origins.BottomLeft + (rayConfig.RaySpacingY * i);
                Vector2 dir = Vector2.left;
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hits: {hits.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.black);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        results.Add(hit.collider);
                    }
                }
            }
            return results;
        }

        public List<Collider2D> CastAllRight(float dist, RaycastConfiguration rayConfig)
        {
            rayConfig.UpdateRaycastOrigins();
            List<Collider2D> results = new();
            for (int i = 0; i < rayConfig.RaycastCountHorizontal; i++)
            {
                Vector2 origin = rayConfig.Origins.BottomRight + (rayConfig.RaySpacingY * i);
                Vector2 dir = Vector2.right;
                RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, dist, _collisionMask);
                // Debugging
                // Debug.Log($"Got hits: {hits.collider.name}");
                Debug.DrawRay(origin, dir * dist, Color.black);
                // Debug.Log($"GroundProbe, origin: {origin}, vector: {dir * dist}");
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit)
                    {
                        results.Add(hit.collider);
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Helper method for performing a raycast from a specified face on an object.
        /// </summary>
        /// <param name="face"></param>
        /// <param name="dist"></param>
        /// <param name="dir"></param>
        /// <param name="rayConfig"></param>
        /// <returns></returns>
        public RaycastHit2D FaceCast(ColliderFace face, float dist, Vector2 dir, RaycastConfiguration rayConfig, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            int raycastCount = GetFaceCount(face, rayConfig);
            Vector2 origin = GetOrigin(face, rayConfig);
            Vector2 spacing = GetSpacing(face, rayConfig);
            for (int i = 0; i < raycastCount; i++)
            {
                Vector2 rayOrigin = origin + (spacing * i);
                RaycastHit2D hits = Physics2D.Raycast(rayOrigin, dir, dist, rayConfig.CollisionLayer);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(rayOrigin, dir * dist, debugColor, 1f);
                }
                if (hits)
                {
                    return hits;
                }
            }
            return new RaycastHit2D();
        }

        public RaycastHit2D FaceCast(ColliderFace face, float dist, Vector2 dir, RaycastConfiguration rayConfig, LayerMask layerMask, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            int raycastCount = GetFaceCount(face, rayConfig);
            Vector2 origin = GetOrigin(face, rayConfig);
            Vector2 spacing = GetSpacing(face, rayConfig);
            for (int i = 0; i < raycastCount; i++)
            {
                Vector2 rayOrigin = origin + (spacing * i);
                RaycastHit2D hits = Physics2D.Raycast(rayOrigin, dir, dist, layerMask);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(rayOrigin, dir * dist, debugColor, 1f);
                }
                if (hits)
                {
                    return hits;
                }
            }
            return new RaycastHit2D();
        }

        public RaycastHit2D FaceCast(ColliderFace face, float dist, Vector2 dir, RaycastConfiguration rayConfig, int layerMask, Color debugColor, bool drawDebug = false)
        {
            rayConfig.UpdateRaycastOrigins();
            int raycastCount = GetFaceCount(face, rayConfig);
            Vector2 origin = GetOrigin(face, rayConfig);
            Vector2 spacing = GetSpacing(face, rayConfig);
            for (int i = 0; i < raycastCount; i++)
            {
                Vector2 rayOrigin = origin + (spacing * i);
                RaycastHit2D hits = Physics2D.Raycast(rayOrigin, dir, dist, layerMask);
                // Debugging
                if (drawDebug)
                {
                    Debug.DrawRay(rayOrigin, dir * dist, debugColor, 1f);
                }
                if (hits)
                {
                    return hits;
                }
            }
            return new RaycastHit2D();
        }

        private int GetFaceCount(ColliderFace face, RaycastConfiguration rayConfig)
        {
            if (face == ColliderFace.Right || face == ColliderFace.Left)
            {
                return rayConfig.RaycastCountHorizontal;
            }
            return rayConfig.RaycastCountVertical;
        }

        private Vector2 GetOrigin(ColliderFace face, RaycastConfiguration rayConfig)
        {
            switch (face)
            {
                case ColliderFace.Left: return rayConfig.Origins.BottomLeft;

                case ColliderFace.Right: return rayConfig.Origins.BottomRight;

                case ColliderFace.Top: return rayConfig.Origins.TopLeft;

                case ColliderFace.Bottom: return rayConfig.Origins.BottomLeft;
            }
            throw new InvalidEnumArgumentException();
        }

        private Vector2 GetSpacing(ColliderFace face, RaycastConfiguration rayConfig)
        {
            if (face == ColliderFace.Right || face == ColliderFace.Left)
            {
                return rayConfig.RaySpacingY;
            }
            return rayConfig.RaySpacingX;
        }
        // public void ResetCollisions()
        // {
        //     CollisionData.Reset();
        // }



        // public void CalculateSpacing()
        // {
        //     Bounds bounds = _collider.bounds;
        //     bounds.Expand(SKIN_WIDTH * -2);

        //     float xSpacing = bounds.size.x / (RaycastCount - 1);
        //     // float ySpacing = bounds.size.y / (RaycastCountHorizontal - 1);

        //     rayConfig.RaySpacingX = new Vector2(xSpacing, 0);
        //     // rayConfig.RaySpacingY = new Vector2(0, ySpacing);
        // }

        // public void UpdateRaycastOrigins()
        // {
        //     Bounds bounds = _collider.bounds;
        //     bounds.Expand(SKIN_WIDTH * -2);

        //     rayConfig.Origins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
        //     rayConfig.Origins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
        //     rayConfig.Origins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        //     rayConfig.Origins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
        //     // DrawCollider(rayConfig.Origins);
        // }
    }
}