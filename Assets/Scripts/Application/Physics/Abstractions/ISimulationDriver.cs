using Physics.Core.DataStructures;

namespace Physics.Application.Abstractions
{
    public interface ISimulationDriver
    {
        public ActorFrameData Step(ActorFrameData frameData, float dt);
        // public void AdvanceSimulation(ActorFrameData frameData);
    }
}