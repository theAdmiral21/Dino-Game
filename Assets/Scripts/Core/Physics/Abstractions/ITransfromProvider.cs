using Primitives.Physics;

namespace Physics.Core.Abstractions
{
    public interface ITransformProvider
    {
        PhysicsTransform GetTransform();
    }
}