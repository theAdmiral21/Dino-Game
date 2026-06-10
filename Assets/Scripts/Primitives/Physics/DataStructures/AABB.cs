using UnityEngine;
namespace Primitives.Physics
{
    public class AABB
    {
        public Vector2 Center;
        public Vector2 Extents;

        public Vector2 Max => Center + Extents;
        public Vector2 Min => Center - Extents;

        public Vector2 Size => Extents * 2;

        // Axis aligned values of faces
        public float Right => Center.x + Extents.x;
        public float Left => Center.x - Extents.x;
        public float Top => Center.y + Extents.y;
        public float Bottom => Center.y - Extents.y;

        public bool Intersects(AABB other)
        {
            return
                Mathf.Abs(Center.x - other.Center.x) <= (Extents.x + other.Extents.x) &&
                Mathf.Abs(Center.y - other.Center.y) <= (Extents.y + other.Extents.y);
        }

        public void Expand(float amount)
        {
            amount *= 0.5f;
            Extents += new Vector2(amount, amount);
        }

    }
}