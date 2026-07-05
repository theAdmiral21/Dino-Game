using AI.Core.Behavior;
using Core.Ai.Behavior.Visualization;

namespace Core.Ai.BlackBoard
{
    public interface IRaptorController
    {
        public IInspectableNode RootNode { get; }
        public void TickBehaviorTree(float dt);
        public void StopBehaviorTree();
        public void StartBehaviorTree();
    }
}