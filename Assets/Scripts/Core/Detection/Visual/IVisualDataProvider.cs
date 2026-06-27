using UnityEngine;
using Primitives.Health;

namespace Core.Detection.Visual
{
    public interface IVisualDataProvider
    {
        float Facing { get; }
        Vector2 Velocity { get; }
        HealthState Health { get; }
    }
}