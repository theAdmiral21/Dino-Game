using System;
using UnityEngine;

namespace AI.Core.PathFinding
{
    public interface IPathData
    {
        public Type PathType { get; }
        public Vector2 Bearing { get; }
    }
}