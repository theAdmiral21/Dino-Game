using Primitives.Physics;

namespace Physics.Core.Abstractions
{
    public interface IBoundsProvider
    {
        AABB GetBounds();
    }
}