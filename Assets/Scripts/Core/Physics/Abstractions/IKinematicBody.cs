using Physics.Core.DataStructures;
using Primitives.Physics;

namespace Physics.Core.Abstractions
{
    public interface IKinematicBody
    {
        public IBoundsProvider Bounds { get; }
        public ITransformProvider TransformProvider { get; }

        public RaycastConfiguration RayConfig { get; }
        public BodyType BodyType { get; }
    }
}