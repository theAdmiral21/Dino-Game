using UnityEngine;

namespace AI.Core.PathFinding
{
    public interface IPathFindContext
    {
        public IPathData FindPath(Vector2 relevantPosition);
    }
}