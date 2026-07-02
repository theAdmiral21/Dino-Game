using Unity.AI.BehaviorTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;

namespace Editor.BehaviorNodeTools.BehaviorView.Elements
{
    public abstract class BaseNode : Node
    {
        public string NodeName { get; set; }
        public Port OutputPort;
        public Port InputPort;
        protected BehaviorNodeSO _nodeData;
        public readonly BaseNode Parent;
        public BaseNode(BehaviorNodeSO nodeData, BaseNode parent)
        {
            _nodeData = nodeData;

            NodeName = _nodeData.name;

            Parent = parent;
        }

        public abstract void Draw();
    }
}