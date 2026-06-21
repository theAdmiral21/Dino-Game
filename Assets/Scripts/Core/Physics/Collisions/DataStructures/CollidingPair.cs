using Game.Core.Effects;
using Physics.Core.DataStructures;
using Physics.Core.PhysicsActors;
using Primitives.Physics;
using UnityEngine;

namespace Core.Physics.Collisions.DataStructures
{
    public class CollidingPair
    {
        public IPhysicsActor ActorA;
        public AABB BoundsA => ActorA.Body.Bounds.GetBounds();
        public IPhysicsActor ActorB;
        public AABB BoundsB
        {
            get
            {
                if (ActorB != null)
                {
                    return ActorB.Body.Bounds.GetBounds();
                }
                return null;
            }
        }

        // Overlap is with respect to body A
        public Vector2 SeparationVector => CalcPenetration();
        public RayCollision Collision;

        public CollidingPair(IPhysicsActor bodyA, IPhysicsActor bodyB)
        {
            ActorA = bodyA;
            ActorB = bodyB;

            // // Calculate overlap
            // CalcPenetration();
        }

        public CollidingPair(IPhysicsActor bodyA, RayCollision rayCollision)
        {
            ActorA = bodyA;
            ActorB = null; ;
            Collision = rayCollision;
            // // Calculate overlap
            // CalcPenetration();
        }

        public CollisionInfo CollisionInfoA()
        {
            ISurfaceTag surfaceTag = null;
            if (Collision.HitInfo)
            {
                Collision.HitInfo.collider.TryGetComponent(out surfaceTag);
            }
            return new CollisionInfo
            {
                CollisionPoint = Vector2.zero,
                Normal = SeparationVector,
                Collider = Collision.HitInfo.collider,
                OtherActor = ActorB,
                Surface = surfaceTag.Tag,
            };
        }

        public CollisionInfo CollisionInfoB()
        {
            Debug.Log($"Collision info: {Collision.HitInfo.collider}");
            ISurfaceTag surfaceTag = null;
            if (Collision.HitInfo)
            {
                Collision.HitInfo.collider.TryGetComponent(out surfaceTag);
            }

            return new CollisionInfo
            {
                CollisionPoint = Collision.HitInfo.point,
                Normal = Collision.HitInfo.normal,
                Collider = Collision.HitInfo.collider,
                OtherActor = ActorA,
                Surface = surfaceTag.Tag,
            };
            // }
            // else
            // {
            //     return new CollisionInfo
            //     {
            //         CollisionPoint = Vector2.zero,
            //         Normal = SeparationVector,
            //         Collider = null,
            //         OtherActor = ActorA,
            //         Surface = Primitives.Audio.SurfaceType.None,
            //     };
            // }
        }

        private Vector2 CalcPenetration()
        {
            if (BoundsB == null) return Vector2.zero;

            // Direction vector (dx,dy) and penetration vector (px,py)
            float dx = BoundsB.Center.x - BoundsA.Center.x;
            float px = (BoundsB.Extents.x + BoundsA.Extents.x) - Mathf.Abs(dx);

            float dy = BoundsB.Center.y - BoundsA.Center.y;
            float py = (BoundsB.Extents.y + BoundsA.Extents.y) - Mathf.Abs(dy);

            // Pick the smaller of the two
            if (px < py)
            {
                return new Vector2(Mathf.Sign(dx) * px, 0);
            }
            else
            {
                return new Vector2(0, Mathf.Sign(dy) * py);
            }
        }
        public bool Equals(CollidingPair other)
        {
            if (other is null) return false;
            return (ReferenceEquals(ActorA, other.ActorA) && ReferenceEquals(ActorB, other.ActorB))
                || (ReferenceEquals(ActorA, other.ActorB) && ReferenceEquals(ActorB, other.ActorA));
        }
        public override bool Equals(object obj) => obj is CollidingPair other && Equals(other);

        public override int GetHashCode()
        {
            // Order-independent hash: combine via XOR or sorted hash codes
            int hashA = ActorA?.GetHashCode() ?? 0;
            int hashB = ActorB?.GetHashCode() ?? 0;
            return hashA ^ hashB; // XOR is naturally order-independent
        }
    }
}