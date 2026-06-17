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
        public AABB BoundsB => ActorB.Body.Bounds.GetBounds();

        // Overlap is with respect to body A
        public Vector2 SeparationVector { get; private set; }

        public CollidingPair(IPhysicsActor bodyA, IPhysicsActor bodyB)
        {
            ActorA = bodyA;
            ActorB = bodyB;

            // Calculate overlap
            CalcPenetration();
        }

        private void CalcPenetration()
        {
            // Direction vector (dx,dy) and penetration vector (px,py)
            float dx = BoundsB.Center.x - BoundsA.Center.x;
            float px = (BoundsB.Extents.x + BoundsA.Extents.x) - Mathf.Abs(dx);

            float dy = BoundsB.Center.y - BoundsA.Center.y;
            float py = (BoundsB.Extents.y + BoundsA.Extents.y) - Mathf.Abs(dy);

            // Pick the smaller of the two
            if (px < py)
            {
                SeparationVector = new Vector2(Mathf.Sign(dx) * px, 0);
            }
            else
            {
                SeparationVector = new Vector2(0, Mathf.Sign(dy) * py);
            }
        }
    }
}