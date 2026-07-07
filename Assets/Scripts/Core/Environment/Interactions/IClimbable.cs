using UnityEngine;
using Primitives.Physics.Enums;

namespace Core.Environment.Interactions
{
    public interface IClimbable
    {
        public Vector2 Center { get; }
        public ClimbObject ClimbSurface { get; }
        public ClimbType GetClimbType(Vector2 climberPos);
    }
}