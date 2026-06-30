using System.Collections.Generic;
using AI.Core.Behavior;

namespace Core.Ai.Behavior.Visualization
{
    public interface IInspectableNode
    {
        public string DisplayName { get; }
        public float LastTickTime { get; }
        public NodeResult LastResult { get; }
        public IReadOnlyList<IInspectableNode> Children { get; }
    }
}