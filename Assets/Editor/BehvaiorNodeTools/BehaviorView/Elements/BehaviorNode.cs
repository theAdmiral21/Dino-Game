using Unity.AI.BehaviorTree;
using UnityEditor.Experimental.GraphView;

namespace Editor.BehaviorNodeTools.BehaviorView.Elements
{
    public class BehaviorNode : BaseNode
    {
        public BehaviorNode(BehaviorNodeSO nodeData, BaseNode parent) : base(nodeData, parent)
        {
            // add an input port on the left for composites to connect to
            InputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            InputPort.portName = "Parent Connection";

            inputContainer.Add(InputPort);
        }

        public void Initialize()
        {
            Draw();
        }

        public override void Draw()
        {
            title = NodeName;


        }
    }
}