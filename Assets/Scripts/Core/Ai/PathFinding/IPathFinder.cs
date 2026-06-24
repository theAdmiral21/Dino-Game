using UnityEngine;

namespace AI.Core.PathFinding
{
    public interface IPathFinder
    {
        IPathData PathFind(Vector2 currentPosition);
    }
}