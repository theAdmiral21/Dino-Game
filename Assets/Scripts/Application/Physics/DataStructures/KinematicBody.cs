using System.Diagnostics;
using Physics.Core.Abstractions;
using Physics.Core.DataStructures;
using Primitives.Physics;

namespace Physics.Application.DataStructures
{
    public class KinematicBody : IKinematicBody
    {
        public IBoundsProvider Bounds { get; private set; }
        public ITransformProvider TransformProvider { get; private set; }
        public RaycastConfiguration RayConfig { get; private set; }
        public BodyType BodyType { get; private set; }

        public KinematicBody(
            IBoundsProvider bounds,
            ITransformProvider transformProvider,
            RaycastConfiguration rayConfig,
            BodyType bodyType
            )
        {
            Bounds = bounds;
            TransformProvider = transformProvider;
            RayConfig = rayConfig;
            BodyType = bodyType;
        }
    }
}