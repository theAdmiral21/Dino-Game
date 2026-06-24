using UnityEngine;

namespace AI.Core.PathFinding
{
    public interface IPathTowards
    {
        IPathData PathToward(Vector2 destination);
    }
}