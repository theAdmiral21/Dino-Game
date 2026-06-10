using Movement.Core.Movement.DataStructures;
using Primitives.Physics;

namespace Movement.Core.Abstractions
{
    public interface IExternalCalculation
    {

        public IActionRequest Calculate(KinematicResult kinematicState);
    }
}