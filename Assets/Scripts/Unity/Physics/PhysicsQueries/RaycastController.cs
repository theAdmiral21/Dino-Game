using System.Collections.Generic;
using System.Linq;
using Codice.CM.Client.Differences;
using Physics.Application.Abstractions;
using Physics.Application.DataStructures;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Physics.Core.PhysicsQueries;
using Physics.Unity.PhysicsQueries;
using Primitives.Physics.DataStructures;
using Unity.Collections;
using UnityEngine;

namespace Physics.Unity.Movement
{
    public class RaycastController : MonoBehaviour, IRaycastController
    {
        public bool DrawRaycast;
        public bool DrawCollider;
        // public bool PrintCollisions;
        // public CollisionInfo CollisionInfo;

        protected LayerMask _collisionMask;

        // public Collider2D Collider => _collider;
        // protected Collider2D _collider;
        protected float _rayCastLengthX;
        protected float _rayCastLengthY;

        private List<RaycastResult> _verticalRaycasts = new();
        private List<RaycastResult> _horizontalRaycasts = new();
        private HashSet<Collider2D> _verticalHits = new();
        private HashSet<Collider2D> _horizontalHits = new();

        private ICornerResolver _cornerResolver;

        protected float _wallJumpCheckDist = 0.1f; // pixels I think
        public virtual void Awake()
        {
            _collisionMask = LayerMask.GetMask("Collision");
            _cornerResolver = new CornerCorrection();
        }

        public void ResetCollisions()
        {
            // CollisionInfo.Reset();
        }

        public void SetCollisionMask(LayerMask mask)
        {
            _collisionMask = mask;
        }

        public Vector2 VerticalRaycast(ref Vector2 velocity, ref RaycastConfiguration rayConfig)
        {
            // Calculate the length of the ray
            _rayCastLengthY = Mathf.Abs(velocity.y) + rayConfig.SkinWidth;
            _verticalRaycasts.Clear();
            _verticalHits.Clear();
            for (int i = 0; i < rayConfig.RaycastCountVertical; i++)
            {
                Vector2 origin = velocity.y <= 0 ? rayConfig.Origins.BottomLeft + (rayConfig.RaySpacingX * i) : rayConfig.Origins.TopLeft + (rayConfig.RaySpacingX * i);
                Vector2 dir = velocity.y <= 0 ? Vector2.down : Vector2.up;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, _rayCastLengthY, _collisionMask);
                if (hit)
                {
                    velocity.y = (hit.distance - rayConfig.SkinWidth) * dir.y;
                    _rayCastLengthY = hit.distance;
                    // Update collision info
                    // CollisionInfo.Below = dir.y == -1;
                    // CollisionInfo.Above = dir.y == 1;
                    _verticalHits.Add(hit.collider);
                    Debug.Log($"Got vertical collision with {hit.collider.name}");
                }

                // Save this frame's result
                var result = new RaycastResult
                {
                    Origin = origin,
                    GotHit = hit.collider != null,
                    Distance = _rayCastLengthY,
                    Direction = dir
                };
                _verticalRaycasts.Add(result);

                if (DrawRaycast)
                {
                    Debug.DrawRay(origin, dir * _rayCastLengthY, Color.green);
                }
                if (DrawCollider)
                {
                    ColliderDraw(rayConfig.Origins);
                }
            }

            // Check for corners
            int cornerIndex = _cornerResolver.CornerCheck(_verticalRaycasts);
            // Debug.Log($"cornerIndex; {cornerIndex}; Vertical count: {_verticalRaycasts.Count}");

            Vector2 correction = Vector2.zero;
            if (cornerIndex != -1 && cornerIndex <= _verticalRaycasts.Count)
            {
                // Debug.Log($"Vertical count: {_verticalRaycasts.Count}");
                // Debug.Log($"Found corner at: {_verticalRaycasts[cornerIndex].Origin}");
                // here is where we would calculate the nudge
                correction = _cornerResolver.CalculateVerticalNudge(_verticalRaycasts[cornerIndex], rayConfig);

                // Here is where we would apply the nudge, but seeing as how the raycast controller doesn't know about the player's transform, I'm not sure how to move the player
                // Debug.Log($"Got correction vector: {correction}");
                // velocity += correction; // so this isn't technically a nudge, but it kind of works
            }
            return correction;
        }

        public Vector2 HorizontalRaycast(ref Vector2 velocity, ref RaycastConfiguration rayConfig)
        {
            // Calculate the length of the ray
            _rayCastLengthX = Mathf.Abs(velocity.x) + rayConfig.SkinWidth;
            _horizontalRaycasts.Clear();
            _horizontalHits.Clear();
            for (int i = 0; i < rayConfig.RaycastCountHorizontal; i++)
            {
                Vector2 origin = velocity.x < 0 ? rayConfig.Origins.BottomLeft + (rayConfig.RaySpacingY * i) : rayConfig.Origins.BottomRight + (rayConfig.RaySpacingY * i);
                Vector2 dir = velocity.x < 0 ? Vector2.left : Vector2.right;
                float castDist = _rayCastLengthX;
                RaycastHit2D hit = Physics2D.Raycast(origin, dir, castDist, _collisionMask);
                if (hit)
                {

                    velocity.x = (hit.distance - rayConfig.SkinWidth) * dir.x;
                    _rayCastLengthX = hit.distance;
                    // Update collision info
                    // CollisionInfo.Left = dir.x == -1;
                    // CollisionInfo.Right = dir.x == 1;

                    // This is where I would track collisions but doing that cleanly is tricky. I'm thinking that I can have a collider2d hash set and just add unique hit.colliders that way when the list is returned you only get the unique collisions. But after that I'm not sure when to look up the actors who collided. I might be able to do something clever but we will see.

                    _horizontalHits.Add(hit.collider);
                    Debug.Log($"Got horizontal collision with {hit.collider.name}");

                }

                // Save this frame's result
                var result = new RaycastResult
                {
                    Origin = origin,
                    GotHit = hit.collider != null,
                    Distance = _rayCastLengthY,
                    Direction = dir
                };
                _horizontalRaycasts.Add(result);

                if (DrawRaycast)
                {
                    Debug.DrawRay(origin, dir * _rayCastLengthX, Color.green);
                }
                if (DrawCollider)
                {
                    ColliderDraw(rayConfig.Origins);
                }
            }

            // Check for corners
            int cornerIndex = _cornerResolver.CornerCheck(_horizontalRaycasts);
            Vector2 correction = Vector2.zero;
            if (cornerIndex != -1 && cornerIndex <= _horizontalRaycasts.Count)
            {
                correction = _cornerResolver.CalculateHorizontalNudge(_horizontalRaycasts[cornerIndex], rayConfig);
            }
            return correction;
        }

        public List<IPhysicsActor> GetCollisions()
        {
            Debug.Log($"Gathering raycast collisions");
            // Combine the sets
            _horizontalHits.UnionWith(_verticalHits);
            List<IPhysicsActor> actors = new();
            foreach (var collider in _horizontalHits)
            {
                if (collider.TryGetComponent(out IPhysicsActor actor))
                {
                    actors.Add(actor);
                }
            }

            return actors;
        }

        public void CornerRayCast(ref Vector2 velocity, ref RaycastConfiguration rayConfig)
        {
            // You could probably cache these to speed up calcs
            // NOTE You treat everything as positive to make adding or subtracting rayConfig.SkinWidth easier later
            _rayCastLengthX = Mathf.Abs(velocity.x) + rayConfig.SkinWidth;
            float dirX = Mathf.Sign(velocity.x);
            _rayCastLengthY = Mathf.Abs(velocity.y) + rayConfig.SkinWidth;
            float dirY = Mathf.Sign(velocity.y);

            // NOTE You don't need a for loop here because you only need to check one of the corners
            Vector2 origin = Vector2.zero;
            if (dirX < 0 && dirY < 0)
            {
                origin = rayConfig.Origins.BottomLeft;
            }
            if (dirX < 0 && dirY > 0)
            {
                origin = rayConfig.Origins.TopLeft;
            }
            if (dirX > 0 && dirY > 0)
            {
                origin = rayConfig.Origins.TopRight;
            }
            if (dirX > 0 && dirY < 0)
            {
                origin = rayConfig.Origins.BottomRight;
            }
            if (origin == Vector2.zero)
            {
                Debug.LogError("[RaycastController] Could not determine origin for corner cast.");
                return;
            }

            // Check for a collision
            RaycastHit2D hit = Physics2D.Raycast(origin, velocity.normalized, velocity.magnitude, _collisionMask);
            if (hit)
            {
                // Debug.Log($"Corner collision: {hit.collider.name}");
                velocity.x = hit.distance * velocity.normalized.x - rayConfig.SkinWidth;
                velocity.y = hit.distance * velocity.normalized.y - rayConfig.SkinWidth;
            }
            Debug.DrawRay(origin, velocity, Color.green);
        }


        /// <summary>
        ///  Method for checking if the given collider will fit within an area centered on the given point.
        /// </summary>
        /// <param name="center"></param>
        public bool CheckFit(Vector2 center, ref RaycastConfiguration rayConfig)
        {
            if (DrawRaycast)
            {
                // Draw a line from your current center to the new center
                Debug.DrawLine(rayConfig.Bounds.Center, center, Color.green, 2f);
                // Draw the collider centered on the destination
                DrawColliderAtPoint(center, rayConfig);
            }

            Collider2D overlap = Physics2D.OverlapBox(center, rayConfig.Bounds.Size, 0f, _collisionMask);
            if (overlap != null)
            {
                Debug.Log($"Overlapped with: {overlap.name}");
            }

            return overlap == null;
        }

        private void ColliderDraw(RaycastOrigins raycastOrigins)
        {
            Debug.DrawLine(raycastOrigins.BottomLeft, raycastOrigins.TopLeft, Color.purple);
            Debug.DrawLine(raycastOrigins.BottomLeft, raycastOrigins.BottomRight, Color.purple);
            Debug.DrawLine(raycastOrigins.TopRight, raycastOrigins.TopLeft, Color.purple);
            Debug.DrawLine(raycastOrigins.BottomRight, raycastOrigins.TopRight, Color.purple);
        }

        private void DrawColliderAtPoint(Vector2 center, RaycastConfiguration rayConfig)
        {
            Vector3 extent = rayConfig.Bounds.Extents;
            Vector2 bottomLeft = new Vector2(center.x - extent.x, center.y - extent.y);
            Vector2 bottomRight = new Vector2(center.x + extent.x, center.y - extent.y);
            Vector2 topLeft = new Vector2(center.x - extent.x, center.y + extent.y);
            Vector2 topRight = new Vector2(center.x + extent.x, center.y + extent.y);

            Debug.DrawLine(bottomLeft, bottomRight, Color.green, 2f);
            Debug.DrawLine(bottomLeft, topLeft, Color.green, 2f);
            Debug.DrawLine(bottomRight, topRight, Color.green, 2f);
            Debug.DrawLine(topLeft, topRight, Color.green, 2f);
        }
    }
}