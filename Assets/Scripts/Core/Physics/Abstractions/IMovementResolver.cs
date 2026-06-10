using Physics.Core.DataStructures;
using UnityEngine;

namespace Physics.Core.Abstractions
{
    public interface IMovementResolver
    {
        public MovementResolution ResolveMovement(Vector2 velocity, RaycastConfiguration rayconfig);
    }
}