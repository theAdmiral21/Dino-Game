using UnityEngine;
using Primitives.Health;
using Primitives.Detection;

namespace Core.Detection.Visual
{
    public interface IVisualDataProvider
    {
        Vector2 Facing { get; }
        Vector2 Velocity { get; }
        HealthState Health { get; }
        PlayerStatus Status { get; }
    }
}