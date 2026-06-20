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

        public CollidingPair(IPhysicsActor bodyA, IPhysicsActor bodyB)
        {
            ActorA = bodyA;
            ActorB = bodyB;

            // // Calculate overlap
            // CalcPenetration();
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