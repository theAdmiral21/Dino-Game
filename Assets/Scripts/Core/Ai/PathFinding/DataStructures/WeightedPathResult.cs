using System;
using UnityEngine;

namespace AI.Core.PathFinding
{
    public struct WeightedPathResult : IPathData
    {
        public Type PathType => typeof(WeightedPathResult);
        public Vector2 Bearing { get; private set; }

        public WeightedPathResult(Vector2 bearing) => Bearing = bearing;
    }
}