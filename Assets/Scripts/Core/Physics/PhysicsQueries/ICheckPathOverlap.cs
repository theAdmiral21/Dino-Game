using Physics.Core.DataStructures;
using UnityEngine;

namespace Physics.Core.PhysicsQueries
{
    public interface ICheckPathOverlap
    {
        public PathOverlap CalcOverlap(RaycastConfiguration moverRays, Vector2 frameDelta);
    }
}