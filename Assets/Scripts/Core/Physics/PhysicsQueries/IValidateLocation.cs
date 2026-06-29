using UnityEngine;

namespace Core.Physics.PhysicsQueries
{
    public interface IValidateLocation
    {
        public bool IsOutsideWall(Vector2 point);
    }
}