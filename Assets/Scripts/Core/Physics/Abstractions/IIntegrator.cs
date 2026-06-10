using Primitives.Physics;

namespace Physics.Core.Abstractions
{
    public interface IIntegrator
    {
        public KinematicResult Integrate(ref KinematicResult kinematicState);
    }
}