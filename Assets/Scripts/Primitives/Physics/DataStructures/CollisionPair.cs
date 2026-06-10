using UnityEngine;

namespace Primitives.Physics.DataStructures
{
    public struct CollisionPair
    {
        public PhysicsBody Body;
        public PhysicsBody OtherBody;
        public CollisionManifold Manifold;

        public Vector2 Normal;
        public float Penetration;

        public CollisionPair(PhysicsBody body, PhysicsBody otherBody)
        {
            Body = body;
            OtherBody = otherBody;
            Manifold = new();

            Normal = Vector2.zero;
            Penetration = 0;

            SolveNormalAndPen();
        }

        public void ApplyCorrection(Vector2 correction)
        {
            // Body.KinematicState.CurrentPosition -= correction * 0.5f;
            // OtherBody.KinematicState.CurrentPosition += correction * 0.5f;
        }

        private void SolveNormalAndPen()
        {
            // Compute the normal 
            AABB a = Body.Bounds;
            AABB b = OtherBody.Bounds;
            Vector2 centerDiff = a.Center - b.Center;

            float xOverlap = (a.Size.x / 2 + b.Size.x / 2) - Mathf.Abs(centerDiff.x);
            float yOverlap = (a.Size.y / 2 + b.Size.y / 2) - Mathf.Abs(centerDiff.y);

            if (xOverlap < yOverlap)
            {
                Normal = new Vector2(Mathf.Sign(centerDiff.x), 0);
                Penetration = xOverlap;
            }
            else
            {
                Normal = new Vector2(0, Mathf.Sign(centerDiff.y));
                Penetration = yOverlap;
            }
        }
    }
}