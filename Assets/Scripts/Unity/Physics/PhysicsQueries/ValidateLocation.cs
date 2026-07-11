using Core.Physics.PhysicsQueries;
using UnityEngine;

namespace Unity.Physics.PhysicsQueries
{
    public class ValidateLocation : IValidateLocation
    {
        public bool IsOutsideWall(Vector2 point)
        {
            int layerMask = LayerMask.GetMask("Collision");
            Collider2D hit = Physics2D.OverlapPoint(point, layerMask);
            return hit == null;
        }
    }
}