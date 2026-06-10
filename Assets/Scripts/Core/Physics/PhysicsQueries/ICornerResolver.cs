using System.Collections.Generic;
using Physics.Core.DataStructures;
using UnityEngine;

namespace Physics.Core.PhysicsQueries
{
    public interface ICornerResolver
    {
        public int CornerCheck(List<RaycastResult> raycasts);
        Vector2 CalculateHorizontalNudge(RaycastResult cornerRay, RaycastConfiguration rayConfig);
        Vector2 CalculateVerticalNudge(RaycastResult cornerRay, RaycastConfiguration rayConfig);
    }
}