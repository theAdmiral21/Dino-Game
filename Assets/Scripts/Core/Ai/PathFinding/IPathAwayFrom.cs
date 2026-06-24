using UnityEngine;

namespace AI.Core.PathFinding
{
    public interface IPathAwayFrom
    {
        IPathData PathAwayFrom(Vector2 currentPosition, Vector2 threatPosition);
    }
}