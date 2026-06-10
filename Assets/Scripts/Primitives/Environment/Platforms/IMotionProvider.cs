using UnityEngine;

namespace Environment.Platforms.Primitives
{
    public interface IMotionProvider
    {
        string DebugName { get; }
        Vector2 DeltaPosition { get; }
        Vector2 Velocity { get; }
    }
}