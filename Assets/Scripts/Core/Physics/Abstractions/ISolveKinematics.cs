using Physics.Core.DataStructures;
using Primitives.Physics;

namespace Physics.Core.Abstractions
{
    public interface ISolveKinematics
    {
        public KinematicResult Solve(ActorFrameData frameData);
    }
}